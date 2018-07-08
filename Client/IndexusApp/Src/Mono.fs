module Mono

open SciterSharp
open SciterSharp.Interop

open System.Diagnostics

type AssertListener() =
    inherit TraceListener()

    override x.Fail (msg, detail) =
        failwith (msg+detail)

    override x.Write (msg:string) = ()
    override x.WriteLine (msg:string) = ()

let SetupMono () =
    Debug.Listeners.Add(new AssertListener())