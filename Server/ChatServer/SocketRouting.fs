module Socket.Routing

open System
open System.Threading

open Suave

open Suave.Sockets
open Suave.Sockets.Control
open Suave.WebSocket

open UserSocket
open User

open Hopac
open Chessie.ErrorHandling

open Socket.Utility

open Newtonsoft.Json
open Newtonsoft.Json.Serialization


let settings = new JsonSerializerSettings (Error=new EventHandler<ErrorEventArgs> (fun x (y:ErrorEventArgs) -> y.ErrorContext.Handled <- true; ()))

let SocketHandShake (websocket:WebSocket) (ctx:HttpContext) =
    let mutable loop = true

    let us:UserSocket = {Ch=Ch(); Watching=None}
    let send msg = JsonConvert.SerializeObject(msg,settings) |> tobyte |> websocket.send Text <| true

    let uid = ref None

    let channelserver =
        job {
            while loop do
                let! msg = Ch.take us.Ch
                let! _ = send msg |> Job.fromAsync
                ()
            ()
        } |> start

    socket {
        while loop do
            let! msg = websocket.read()

            match msg with
            | (Text, data, true) ->
                let str = UTF8.toString data
                let json = JsonConvert.DeserializeObject<ServerMessage>(str,settings)

                if box json |> isNull then
                    do! send ("Json is invalid." |> fail |> ServerResult)
                else
                    let msg = unbox<ServerMessage> json
                    uid := msg |> HandleMessage !uid us

            | (Close, _, _) ->
                let emptyResponse = [||] |> ByteSegment
                do! websocket.send Close emptyResponse true
                loop <- false

                !uid |> HandleDisconnect

            | _ -> ()
    }