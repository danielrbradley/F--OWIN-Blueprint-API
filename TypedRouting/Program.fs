module Program

open System.Threading.Tasks

open Microsoft.Owin
open Microsoft.Owin.Hosting

open Owin

type Startup () =
    let mapAsyncHandler (handler : IOwinContext -> (unit -> Async<unit>) -> Async<unit>) (context : IOwinContext) (next : unit -> Task) : Task =
        let asyncNext () =
            Async.AwaitIAsyncResult(next())
            |> Async.Ignore
        let taskHandler =
            handler context asyncNext
            |> Async.StartAsTask
        taskHandler :> Task

    let useHandler handler (app : IAppBuilder) =
        app.Use(handler |> mapAsyncHandler)

    member x.Configuration app =
        app
        |> useHandler (TypedRouting.DocumentApi.buildOwinHandler())
        |> ignore

[<EntryPoint>]
let Main(args) = 
    use app = WebApp.Start<Startup>("")
    System.Console.Read() |> ignore
    0
