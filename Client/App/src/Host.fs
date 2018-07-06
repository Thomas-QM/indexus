module Host

open SciterSharp
open SciterSharp.Interop

open System
open System.IO

type HostEvh() =
    inherit SciterEventHandler()

    member x.Host_HelloWorld (el, args, result) =
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

    member x.BaseHost() =
        // #if !DEBUG
        // x.archive.Open(SciterAppResource.ArchiveResource.resources)
        // #endif
        ()

    member x.Setup (newwnd:SciterWindow) =
        wnd <- newwnd
        x.SetupWindow newwnd
        ()

    member x.SetupPage(respage) =
        #if DEBUG
        let path = Environment.CurrentDirectory + "/res/" + respage
        assert File.Exists(path)

        let url = "file:///" + path
        //string url = page_from_res_folder
        #else
        string url = "archive://app/" + respage
        #endif

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