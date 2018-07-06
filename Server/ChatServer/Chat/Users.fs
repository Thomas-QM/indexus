module User
open Hopac
open Chessie.ErrorHandling

open Socket
open User
open UserSocket
open Channel

open Suave.WebSocket

open System

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

job {
    SendMessageInWatch 1L (ok () |> ServerResult)
    do! timeOutMillis 900
} |> Job.foreverServer |> run