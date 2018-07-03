// Learn more about F# at http://fsharp.org

open System
open Newtonsoft.Json

open Socket

[<EntryPoint>]
let main argv =
    JsonConvert.SerializeObject(NewMessage "hello world") |> printfn "%s"
    0 // return an integer exit code
