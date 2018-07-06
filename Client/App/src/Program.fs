module Program
open Host

open SciterSharp
open SciterSharp.Interop


[<EntryPoint>]
let main args =
    let win = new SciterWindow()

    let mutable rc = PInvokeUtils.RECT()
    rc.right <- 800
    rc.bottom <- 600

    win.CreateMainWindow(1500,800)
    win.CenterTopLevelWindow()
    
    let host = new BaseHost()
    host.Setup(win)
    host.AttachEvh(new HostEvh())
    host.SetupPage("login.html") |> ignore
    host.DebugInspect()

    win.Show()
    PInvokeUtils.RunMsgLoop ()

    0