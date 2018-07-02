module Socket

open Message
open Guild

type ClientMessage =
    | NewMessage of Message
    | ServerUpdate of Guild

    | ServerResult of Result<unit, string>

type ServerMessage =
    | NewMessage of string