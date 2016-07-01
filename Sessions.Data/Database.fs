module Database

open System
open System.Configuration

open Dapper
open Dapper.Contrib.Extensions
open MySql.Data.MySqlClient

let getConnection =
    let connectionString = ConfigurationManager.ConnectionStrings.Item("DefaultConnection").ConnectionString
    fun () -> new MySqlConnection(connectionString)

// Dapper.Contrib.Extensions' Data.IDbConnection.Insert<T> seems to have a bug. TODO: Investigate and submit a PR.
let insert entity =
    let entityType = entity.GetType()

    let tableAtt = entityType.GetCustomAttributes(typedefof<TableAttribute>, false).[0] :?> TableAttribute
    let table = tableAtt.Name

    let properties = entityType.GetProperties()
    let propertyNames = properties |> Array.map (fun p -> "@" + p.Name)
    let columnNames = properties |> Array.map (fun p -> p.Name.ToLowerInvariant())

    let sql = "insert " + table + "(" + String.Join(", ", columnNames) + ") values(" + String.Join(", ", propertyNames) + ")"

    getConnection().Execute(sql, entity)

let selectWhere<'Entity> (filters : Collections.Generic.IDictionary<string,obj>)= 
    let entityType = typeof<'Entity>
    let tableAtt = entityType.GetCustomAttributes(typedefof<TableAttribute>, false).[0] :?> TableAttribute
    let table = tableAtt.Name

    let properties = entityType.GetProperties()
    let columnNames = properties |> Array.map (fun p -> p.Name.ToLowerInvariant())

    let selectSql = "select " + String.Join(", ", columnNames) + " from " + table
    if filters.Count > 0 then
        let keysAsParamaters = filters.Keys |> Seq.map (fun key -> key + " = @" + key)
        let whereSql = "where " + String.Join(" and ", keysAsParamaters)
        let sql = selectSql + " " + whereSql
        getConnection().Query<'Entity>(sql,filters)
    else 
        raise <| Exception("Where clause requires at least one filter for selectWhere")    