module Window

open SciterSharp
open System.Drawing


type Window () =
    inherit SciterWindow ()

    member x.Window () =
        x.CreateMainWindow(800, 600)
        x.CenterTopLevelWindow()
        x.Title = "IndexusApp"
        //set icon here, windows only