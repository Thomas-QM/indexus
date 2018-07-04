module Socket

open Chessie.ErrorHandling
open Message
open Channel
open Guild
open Auth

type ClientMessage =
    | Channel of CChannelMsg

    | ServerResult of Result<unit, string>
    | Authorized of AuthToken

type ServerMessage =
    | Channel of SChannelMsg

    | Authorize of Login