module Catch //TODO better name?

open System
open System.Net
open System.Net.Http
open System.Web.Http

let respond (c: ApiController) successCode op =
    try
        let result = op()
        printfn "Action succeeded"
        c.Request.CreateResponse(successCode, op())
    with
    | ex ->
        printfn "Action failed with error: %s" ex.Message
        c.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)



