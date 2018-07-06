module Channel

open Auth
open Message
open Group
open User

type ChannelId = int64
type ChannelMentionId = int64

type InChannel<'A> = ChannelId*'A

type Channel = {ChannelId: ChannelId; Name:string; GroupId:GroupId}
type ChannelUser = {ChannelUserId: UserId; GroupUserId: UserId; ChannelId:ChannelId; Unread:bool;}
type ChannelMention = {ChannelMentionId: ChannelMentionId; ChannelUserId: UserId; MessageId: MessageId;}


type CChannelMsg =
    | NewMessage of Message
    | UnreadMessage of InChannel<Message>

type SChannelMsg =
    | SetActive of ChannelId option

    | NewMessage of Auth<string>
    // | SetChannelFocus of ChannelId
    // | SetTyping of bool