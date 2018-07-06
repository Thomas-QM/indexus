module Auth

open System

type Login = {Username: string; Password: string;}
type AuthToken = Guid
type Auth<'A> = AuthToken*'A