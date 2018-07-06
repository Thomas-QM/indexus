module Data

open LimeBean
open Npgsql

let InitializeConn connstring =
    let api = new BeanApi(connstring,typeof<NpgsqlConnection>)
    api

let CloseConn (conn:NpgsqlConnection) =
    conn.Close ()