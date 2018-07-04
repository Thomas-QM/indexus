module Channel

open Auth
open Message

type ChannelId = int64

type CChannelMsg =
    | NewMessage of Message

type SChannelMsg =
    | NewMessage of Auth<string>
    // | SetChannelFocus of ChannelId
    // | SetTyping of bool