module Auth

open System
open Auth

open Hopac
open Chessie.ErrorHandling
open HashLibrary

let hasher = Hasher ()

let Login user pass =
    ok ()

let AuthToken token =
    1L |> ok

let (|Authorized|_|) ((token,x):Auth<'A>) =
    match AuthToken token with
        | Pass id ->
            Some (x,id)
        | _ -> None