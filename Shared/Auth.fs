module Auth

type Login = {Username: string; Password: string;}
type AuthToken = int64
type Auth<'A> = AuthToken*'A