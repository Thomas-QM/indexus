module Host

open SciterSharp
open SciterSharp.Interop
open System.Runtime.InteropServices

open System
open System.IO

type HostEvh() as x =
    inherit SciterEventHandler()

    member public x.Host_HelloWorld (el:SciterElement, args:SciterValue array, [<Out>] (result:byref<SciterValue>)) =
        result <- new SciterValue("hallo world")
        
        true



// This base class overrides OnLoadData and does the resource loading strategy
// explained at http://misoftware.rs/Bootstrap/Dev
//
// - in DEBUG mode: resources loaded directly from the file system
// - in RELEASE mode: resources loaded from by a SciterArchive (packed binary data contained as C# code in ArchiveResource.cs)
type BaseHost () =
    inherit SciterHost ()
    let mutable wnd = null

    member x.api = SciterX.API
    member x.archive = new SciterArchive()

    member x.Setup (newwnd:SciterWindow) =
        wnd <- newwnd
        x.SetupWindow newwnd
        ()

    member x.SetupPage(respage) =
        let path = Environment.CurrentDirectory + "/../../res/" + respage
        printfn "%s" path
        assert File.Exists(path)

        let url = "file:///" + path
        //string url = page_from_res_folder

        wnd.LoadPage(url)

        //	Debug.Assert(res);

    override x.OnLoadData(sld) =
        if sld.uri.StartsWith("archive://app/") then
            // load resource from SciterArchive
            let path = sld.uri.Substring(14)
            let data = x.archive.Get(path)
            if data |> isNull |> not then
                x.api.SciterDataReady.Invoke (wnd._hwnd, sld.uri, data, (uint32) data.Length) |> ignore
                ()
        base.OnLoadData(sld)