module User
    open Group

    type UserId = int64

    type UserStatus = Online | Offline
    type User = {UserId:UserId; UserStatus:UserStatus; UserName:string;}
    type GroupUser = {GroupUserId:UserId; UserId:UserId; GroupId:GroupId}