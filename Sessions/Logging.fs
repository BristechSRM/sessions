module Logging

open System.Web.Http
open System.Web.Http.Filters
open System.Web.Http.Controllers

type LoggingActionFilter() =
    inherit ActionFilterAttribute()

    override f.OnActionExecuting(context) =
        printfn "Executing action %s on controller %s" context.ActionDescriptor.ActionName context.ControllerContext.ControllerDescriptor.ControllerName

let setupLogging () =
    // TODO: Why doesn't this work?
    GlobalConfiguration.Configuration.Filters.Add(LoggingActionFilter())