module TypedRouting.DocumentApi
open Microsoft.Owin

type IBlueprintApi =
    abstract member GetRoutes : unit -> (string * (IOwinContext -> Async<unit>)) list

// This would be auto-generated inside of the Blueprint type provider
type DocumentApi
    (
        ``GET /docs{?skip}`` : (int * IOwinContext) -> Async<unit>,
        ``GET /docs/{id}`` : (int * IOwinContext) -> Async<unit>
    ) =

    let ``GET /docs{?skip} Query`` (context : IOwinContext) : Async<unit> = async {
        // TODO: Parse arguments.
        do! ``GET /docs{?skip}`` (0, context)
    }

    let ``GET /docs/{id} Get`` (context : IOwinContext) : Async<unit> = async {
        // TODO: Parse arguments.
        do! ``GET /docs{?skip}`` (0, context)
    }

    interface IBlueprintApi with
        member x.GetRoutes () =
            [
                ("GET /docs{?skip}", ``GET /docs{?skip} Query``)
                ("GET /docs/{id} Get", ``GET /docs/{id} Get``)
            ]


// Example of Ideal usage:

//type DocumentApi = Blueprint<"DocumentApi.md">
let documentApi =
    DocumentApi
        (
            ``GET /docs{?skip}`` = fun (skip, context) -> async {
                    context.Response.ContentType <- "text/plain"
                    do! Async.AwaitIAsyncResult(context.Response.WriteAsync("Hello World!\r\nHello World!\r\nHello World!\r\nHello World!")) |> Async.Ignore
                }
            ,
            ``GET /docs/{id}`` = fun (id, context) -> async {
                    context.Response.ContentType <- "text/plain"
                    do! Async.AwaitIAsyncResult(context.Response.WriteAsync("Hello World!")) |> Async.Ignore
                }
        )


// Hacky example of a server configuration
module RouteTable =
    let create =
        Map.empty

    let register routes table =
        let addRoute table (uriTemplate, handler) =
            table |> Map.add uriTemplate handler
        routes |> List.fold addRoute table

    let registerBlueprintApi (blueprintApi : IBlueprintApi) =
        register (blueprintApi.GetRoutes())

    let buildOwinHandler table : IOwinContext -> (unit -> Async<unit>) -> Async<unit> =
        failwith ""

let buildOwinHandler () =
    RouteTable.create
    |> RouteTable.registerBlueprintApi documentApi
    |> RouteTable.buildOwinHandler
