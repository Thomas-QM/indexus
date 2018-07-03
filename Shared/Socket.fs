module Socket

open Chessie.ErrorHandling
open Message
open Guild

type ClientMessage =
    | NewMessage of Message
    | ServerUpdate of Guild

    | ServerResult of Result<unit, string>

    | Authorized of AuthToken

type ServerMessage =
    | NewMessage of Auth<string>
    // | SetChannelFocus of ChannelId
    // | SetTyping of bool

    | Authorize of Login