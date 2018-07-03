module Socket.Utility

open Suave

open Suave.Sockets
open Suave.Sockets.Control
open Suave.WebSocket

open System

let tobyte (s:string) = System.Text.Encoding.ASCII.GetBytes s |> ByteSegment