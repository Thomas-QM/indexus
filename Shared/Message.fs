module Message
    open User

    type MessageId = int64
    type Message = {MessageId: MessageId; UserId: UserId; Text:string; Time:int64} //TODO: add channel