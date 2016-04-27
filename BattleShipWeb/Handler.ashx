<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Threading;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Drawing;

public class Handler : IHttpAsyncHandler
{
    public IAsyncResult BeginProcessRequest(HttpContext ctx, AsyncCallback cb, Object obj)
    {
        AsyncResult currentAsyncState = new AsyncResult(ctx, cb, obj);
        ThreadPool.QueueUserWorkItem(new WaitCallback(RequestWorker), currentAsyncState); 
        return currentAsyncState;
    }

    private void RequestWorker(Object obj)
    {
        AsyncResult myAsyncResult = obj as AsyncResult;
        string command = myAsyncResult._context.Request.QueryString["cmd"];
        string guid = myAsyncResult._context.Request.QueryString["guid"];
        string JsonString_0 = "", JsonString_1 = "";
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        switch (command)
        {
            case "register":
                AsyncServer.RegisterClient(myAsyncResult);
                myAsyncResult.CompleteRequest();
                break;
            case "unregister":
                AsyncServer.UnregisterClient(guid);
                myAsyncResult.CompleteRequest();
                break;
            case "process":
                if (guid != null)
                    AsyncServer.UpdateClient(myAsyncResult, guid);
                break;
            case "loadBoard":
                AsyncServer.LoadBoard(myAsyncResult, guid);
                myAsyncResult.CompleteRequest();
                break;
        }
    }
        
    public void EndProcessRequest(IAsyncResult ar)
    {
    }

    public void ProcessRequest(HttpContext context)
    {
    }
    
    public bool IsReusable {
        get {
            return true;
        }
    }

}