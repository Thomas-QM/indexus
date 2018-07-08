module Data.User

open LimeBean
open System
open Auth

open Hopac
open Chessie.ErrorHandling
open Chessie.Hopac

type DBUser = {Id:int64; Name:string; Hash: string; Salt: string; Session: AuthToken option}

let BeanToUser b =
    let get x = Get b x
    {Id=get "id"; Name=get "name"; Hash=get "hash"; Salt=get "salt"; Session=get "session" |> OptFromNull}

let UserToBean {Id=i; Name=n; Hash=h; Salt=s; Session=se} api =
    Dispense "user" api |> Put "id" i |> Put "name" n |> Put "hash" h |> Put "salt" s |> Put "session" se

let MakeUser user api =
    let u = api |> UserToBean user
    Store u

let DelUser uid api = jobTrial {
    let! u = api |> Load "user" uid
    return! u |> Trash <| api
}

let GetUser uid api = jobTrial {
    let! u = Load "user" uid api
    return u |> BeanToUser
}

let MakeSession uid api = jobTrial {
    let! u = api |> Load "user" uid
    let id = Guid.NewGuid ()
    return! UserToBean { (u |> BeanToUser) with Session=Some id } <| api |> Store <| api
}

let SessionValid uid ses api = jobTrial {
    let! u = api |> Load "user" uid
    return u |> BeanToUser |> (function | {Session=Some x} -> x=ses | _ -> false)
}