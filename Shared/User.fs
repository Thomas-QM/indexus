module User
    type UserId = int64

    type User = {UserId:UserId; UserName:string; UserIcon:string}