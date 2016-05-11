<%@ WebHandler Language="C#" Class="AdminHandler" %>

using System;
using System.Web;
using System.Threading;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Drawing;


public class AdminHandler : IHttpAsyncHandler {

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
        string stringBoard = myAsyncResult._context.Request.QueryString["stringBoard"];        
        //string guid = myAsyncResult._context.Request.QueryString["playerId"];
        string JsonString_0 = "", JsonString_1 = "";
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        switch (command)
        {
            //case "process":
            //    if (guid != null)
            //        AsyncServer.UpdateClient(myAsyncResult, guid);
            //    break;
            case "addBoard":
                AdminAsyncServer.addBoard(myAsyncResult, stringBoard);
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

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}