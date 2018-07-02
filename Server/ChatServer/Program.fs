open System

open Suave
open Suave.Filters
open Suave.Operators
open Suave.WebSocket
open Suave.Successful
open Suave

let config = defaultConfig

let app =
    choose [
        path "/chatserver" >=> handShake SocketRouting.SocketHandShake
    ]

[<EntryPoint>]
let main argv =
    startWebServer config app
    0