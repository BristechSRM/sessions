module Database

open Dapper
open Dapper.Contrib.Extensions
open System.Configuration
open MySql.Data.MySqlClient

let getConnection =
    let connectionString = ConfigurationManager.ConnectionStrings.Item("DefaultConnection").ConnectionString
    fun () -> new MySqlConnection(connectionString)

// TODO: Seems to have a bug in Dapper.Contrib.Extensions. Write our own?
let add entity = getConnection().Insert<_>(entity)

// Utility method to select list based on FK