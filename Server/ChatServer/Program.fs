open Suave
open Suave.Http
open Suave.Operators
open Suave.Filters
open Suave.Successful
open Suave.Files
open Suave.RequestErrors
open Suave.Logging
open Suave.Utils

open System
open System.Net

open Suave.Sockets
open Suave.Sockets.Control
open Suave.WebSocket

let config = {defaultConfig with logger = Targets.create Verbose [||]}


[<EntryPoint>]
let main argv =
    startWebServer config (handShake Socket.Routing.SocketHandShake)
    0