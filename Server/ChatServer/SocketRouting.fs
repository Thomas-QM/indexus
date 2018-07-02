module SocketRouting

open System

open Suave
open Suave.Sockets
open Suave.Sockets.Control
open Suave.WebSocket

let SocketHandShake (socket:WebSocket) =
    socket.w   