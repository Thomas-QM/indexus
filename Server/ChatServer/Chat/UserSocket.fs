module UserSocket

open Hopac
open Chessie.ErrorHandling

open Socket
open User
open Channel

open Suave.WebSocket

open System

type UserSocket = {Ch: Ch<ClientMessage>; Watching: ChannelId option}

let users = Map.empty<int64, UserSocket> |> MVar

let usersc x = users |> MVar.read |> Alt.afterFun x |> run

let SendMessage uid msg =
    usersc (fun x -> x.TryFind uid) |> Option.iter (fun {Ch=x} -> Ch.give x msg |> run)

let SendMessageAll msg =
    usersc (Map.iter (fun _ {Ch=y} -> Ch.give y msg |> run))

let SendMessageInWatch chan msg =
    usersc (Map.iter (fun _ -> function | {Watching=Some x; Ch=y} when x = chan -> Ch.give y msg |> run | _ -> ()))