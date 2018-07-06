module Socket

open Chessie.ErrorHandling
open Message
open Channel
open Group
open Auth

type ClientMessage =
    | Channel of CChannelMsg

    | ServerResult of Result<unit, string>
    | AuthorizedToken of AuthToken

type ServerMessage =
    | Channel of SChannelMsg

    | Authorize of Login