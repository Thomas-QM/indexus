module Users

open Suave.WebSocket

let mutable UserSockets:(ref<WebSocket> array)  = [||]

