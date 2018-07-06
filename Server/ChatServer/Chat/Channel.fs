module Channel

open UserSocket
open Channel
open User
open Auth
open Socket

let MakeNewMessage chan usr txt =
    SendMessageInWatch watch (ClientMessage.NewMessage {})

let HandleChannelMsg us x =
    let {Ch=ch; Watching=watch} = us
    match x, watch with
        | Authorized (NewMessage x, uid), Some watch ->
            