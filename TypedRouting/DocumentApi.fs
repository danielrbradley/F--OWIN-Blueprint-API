module TypedRouting
open Microsoft.Owin

// Placeholder types
type Request = 
    | Get of string
    | Post
    | Put
    | Delete

type Response = {
    StatusCode : int
    Response : string
}

type BlueprintApi =
    abstract member TryHandle : IOwinContext -> bool

// This would actually be nested inside Api
type Foo = {
    skip : int
}

// This would be auto-generated inside of the Blueprint type provider
type DocumentApi
    (
        ``GET /docs{?skip}`` : int -> Response,
        ``GET /docs/{id}`` : int -> Response
    ) =
    interface BlueprintApi with
        member x.TryHandle context =
            failwith "Not Implemented"


// Example of Ideal usage:

//type DocumentApi = Blueprint<"DocumentApi.md">
let documentApi =
    DocumentApi
        (
            ``GET /docs{?skip}`` = fun skip ->
                {
                    StatusCode = 200
                    Response = "Hello world!"
                }
            ,
            ``GET /docs/{id}`` = fun id ->
                {
                    StatusCode = 200
                    Response = sprintf "Hello world!, here's document #%i" id
                }
        )


// Hacky example of a server configuration
module Server =
    type Server = {
        Url : string
        Apis : Map<string option, BlueprintApi>
    }

    let create url =
        {
            Url = url
            Apis = Map.empty
        }

    let registerArea path api server =
        { server with Apis = server.Apis |> Map.add (Some path) api }

    let register api server =
        { server with Apis = server.Apis |> Map.add None api }

    let start server =
        failwith "Not Implemented"

let startExample () =
    Server.create "http://localhost:8080"
    |> Server.register documentApi
    |> Server.start

