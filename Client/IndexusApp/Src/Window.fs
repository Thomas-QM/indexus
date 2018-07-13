module Window

open SciterSharp
open System.Drawing

open Resources

let ConfigureWindow (x:SciterWindow) =
    x.CreateMainWindow(800, 600)
    x.CenterTopLevelWindow()
    x.Title <- "Indexus"
    //set icon here, windows only
    #if WINDOWS
    x.Icon <- Resource.IconMain
    #endif
    x