module Users
open Hopac
open Chessie.ErrorHandling

open Socket
open User

open Suave.WebSocket

open System

type UserSocket = Ch<ClientMessage>

let users = Map.empty<int64, UserSocket> |> MVar

// Job.iterateServer defaultstate <| fun x -> job {
//     printfn "Iterating server..."
//     let! newsocket = Ch.take UserChannel
//     match newsocket with
//         | Register (id, sock) -> return Map.add id sock x
//         | Deregister id -> return Map.remove id x
//         | SendUserMessage (id, msg) ->
//             match Map.tryFind id x with
//                 | Some x -> do! Ch.send x msg
//                 | None -> ()
//             return x
// } |> start

let HandleMessage uid ch msg :UserId option =
    let mutator uid x =
        printfn "%i connected" uid
        Map.add uid ch x

    let newuid = Job.Random.get () |> run |> int64

    MVar.mutateFun (mutator newuid) users |> run
    newuid |> Some

let HandleDisconnect uid =
    let mutator uid x =
        printfn "%i disconnected" uid
        Map.remove uid x

    uid |> Option.iter (fun uid -> MVar.mutateFun (mutator uid) users |> run)

let SendMessage uid msg =
    users |> MVar.read |> Alt.afterFun (fun x -> x.TryFind uid) |> Alt.afterFun (Option.iter (fun x -> Ch.give x msg |> run)) |> run

let SendMessageAll msg =
    users |> MVar.read |> Alt.afterFun (Map.iter (fun id y -> printfn "Sending message to %i" id; Ch.give y msg |> run)) |> run

job {
    SendMessageAll (ok () |> ServerResult)
    do! timeOutMillis 900
} |> Job.foreverServer |> run