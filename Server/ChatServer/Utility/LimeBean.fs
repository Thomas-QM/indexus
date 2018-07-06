module LimeBean

open LimeBean
open Npgsql
open System

open Hopac
open Chessie.ErrorHandling
open Chessie.Hopac

let ToObj x =
    x :> obj

let Dispense x (api:BeanApi) =
    api.Dispense (x)

let Store x (api:BeanApi) =
    api.Store (x) |> ignore |> Job.result

let Key keytype (keyval:string) (api:BeanApi) =
    api.Key (keytype,keyval)
    api

let CustomKey keytype (keyval:string) (api:BeanApi) =
    api.Key (keytype,keyval,false)
    api

let Trash x (api:BeanApi) =
    api.Trash (x) |> ignore |> Job.result

let Count btype sql param (api:BeanApi) =
    api.Count (btype,sql,param) |> Job.result

let Load btype (id:obj) (api:BeanApi) =
    let bean = api.Load (btype,id)
    (if isNull bean then fail "Row not found" else ok bean) |> Job.result |> ofJobOfResult

let Find btype exp param (api:BeanApi) =
    api.Find (btype,exp,param) |> Job.result

let Get<'T> (bean:Bean) (name:string) =
    bean.Get<'T>(name)

let (|Get|) name bean =
    bean |> Get name

let Put key value (bean:Bean) =
    bean.Put (key,value)

let Exec query param (api:BeanApi) =
    api.Exec (query,param) |> ignore |> Job.result