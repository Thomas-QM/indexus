module Program
open Host
open Window

open SciterSharp
open SciterSharp.Interop

[<EntryPoint>]
let main args =
    let win = new SciterWindow () |> ConfigureWindow
    
    let host = new BaseHost()
    host.Setup(win)
    host.AttachEvh(new HostEvh())
    host.SetupPage("Login/login.html") |> ignore

    try
        host.DebugInspect()
    with
        | _ -> ()

    win.Show()
    PInvokeUtils.RunMsgLoop ()

    0