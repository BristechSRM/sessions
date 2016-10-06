module Database

open System
open System.Configuration

open Dapper
open Dapper.Contrib.Extensions
open MySql.Data.MySqlClient
open Microsoft.FSharp.Reflection

//Note: Not importing Collections.Generic as it hides some F# types

let getConnection =
    let connectionString = ConfigurationManager.ConnectionStrings.Item("DefaultConnection").ConnectionString
    fun () -> new MySqlConnection(connectionString)

let private getTableName (entityType : Type) = 
    let tableAtt = entityType.GetCustomAttributes(typedefof<TableAttribute>, false).[0] :?> TableAttribute
    tableAtt.Name

let private getPrimaryKeyColumn<'Entity> () = 
    let entityType = typeof<'Entity>
    let idNamedProp = entityType.GetProperties() |> Array.tryFind (fun p -> p.Name.ToLowerInvariant() = "id")
    match idNamedProp with
    | Some prop -> prop.Name.ToLowerInvariant()
    | None -> 
        let idKeyedProp = 
            entityType.GetProperties() 
            |> Array.tryFind (fun p -> 
                let keyAttribute = p.GetCustomAttributes(typedefof<KeyAttribute>, false).[0] :?> KeyAttribute
                keyAttribute |> isNull |> not)
        match idKeyedProp with
        | Some prop -> prop.Name.ToLowerInvariant()
        | None -> raise <| Exception(sprintf "Specified entity: %s does not have a column specified as 'Id' or a column marked with the KeyAttribute. Cannot perform update." entityType.Name) 


// Dapper.Contrib.Extensions' Data.IDbConnection.Insert<T> seems to have a bug. TODO: Investigate and submit a PR.
let insert entity =
    let entityType = entity.GetType()

    let tableName = getTableName entityType

    let properties = entityType.GetProperties()
    let propertyNames = properties |> Array.map (fun p -> "@" + p.Name)
    let columnNames = properties |> Array.map (fun p -> p.Name.ToLowerInvariant())

    let sql = "insert " + tableName + "(" + String.Join(", ", columnNames) + ") values(" + String.Join(", ", propertyNames) + ")"

    getConnection().Execute(sql, entity)

let selectWhere<'Entity> (filters : Collections.Generic.IDictionary<string,obj>) = 
    if filters.Count <> 0 then
        let entityType = typeof<'Entity>

        let tableName = getTableName entityType

        let properties = entityType.GetProperties()
        let columnNames = properties |> Array.map (fun p -> p.Name.ToLowerInvariant())

        let selectSql = "select " + String.Join(", ", columnNames) + " from " + tableName

        let keysAsParamaters = filters.Keys |> Seq.map (fun key -> key + " = @" + key)
        let whereSql = "where " + String.Join(" and ", keysAsParamaters)
        let sql = selectSql + " " + whereSql
        getConnection().Query<'Entity>(sql,filters)
    else 
        raise <| Exception("Where clause requires at least one filter for selectWhere")    

/// Warning: This is not type safe. The value of newValue may be coerced by the database into the column type. 
/// E.g. Anything can be inserted into a string column. A float will be truncated and inserted into an int column. 
let updateFieldWhere<'Entity> (propName : string) newValue (filters : Collections.Generic.IDictionary<string,obj>) =
    let entityType = typeof<'Entity>
    let tableName = getTableName entityType

    let updateSql = sprintf "update %s set %s = @%s " tableName propName propName

    let keysAsParamaters = filters.Keys |> Seq.map (fun key -> sprintf "%s = @%s " key key)
    let whereSql = "where " + String.Join("and ", keysAsParamaters)
    let sql = updateSql + whereSql

    let parameters = ResizeArray(filters)
    parameters.Add(new Collections.Generic.KeyValuePair<string,obj>(propName, box newValue))

    let result = getConnection().Execute(sql, parameters)
    if result = 0 then
        raise <| Exception(sprintf "%s was not updated. Check that the input filters matches a %s." entityType.Name entityType.Name)
    else 
        result

let updateField<'Entity> recordId (propName : string) newValue = 
    updateFieldWhere<'Entity> propName newValue (dict [ "id", box recordId])

let update<'Entity> (entity : 'Entity) = 
    let entityType = typeof<'Entity>
    let tableName = getTableName entityType

    let updateSql = sprintf "update %s set " tableName
    let idColumn = getPrimaryKeyColumn<'Entity> () 
    let propsAsParameters = 
        entityType.GetProperties() 
        |> Seq.filter(fun p -> p.Name.ToLowerInvariant() <> idColumn)
        |> Seq.map(fun p -> 
            let prop = p.Name.ToLowerInvariant()
            sprintf "%s = @%s " prop prop)
    let whereSql = sprintf "where %s = @%s " idColumn idColumn
    let sql = updateSql + String.Join(", ", propsAsParameters) + whereSql

    let parameters =
        FSharpType.GetRecordFields(entityType)
        |> Array.map(fun p -> new Collections.Generic.KeyValuePair<string,obj>(p.Name.ToLowerInvariant(), p.GetValue(entity)))

    let result = getConnection().Execute(sql, parameters)
    if result = 0 then
        raise <| Exception(sprintf "%s was not updated. Check that the input filters matches a %s." entityType.Name entityType.Name)
    else 
        result
     
/// Currently deleting based on column called Id. Could expand to use Key attribute if necessary ( see https://github.com/StackExchange/dapper-dot-net/tree/master/Dapper.Contrib#special-attributes ) 
let deleteById<'Entity> recordId = 
    let entityType = typeof<'Entity>
    let tableName = getTableName entityType

    let sql = "delete from " + tableName + " where `id`=@id"
    let result = getConnection().Execute(sql, (dict [ "id", box recordId]))
    result = 1
