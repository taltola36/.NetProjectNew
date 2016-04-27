using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Script.Serialization;

public class AsyncServer
{
    private static Object _lock = new Object();
    private static Dictionary<string, AsyncResult> _clientStateList = new Dictionary<string, AsyncResult>();
    private static bool wait = false;
    private static ArrayList arr= new ArrayList();

    public static void RegisterClient(AsyncResult state)
    {
        wait = !wait;
        lock (_lock)
        {
            state.ClientGuid = Guid.NewGuid().ToString();
            _clientStateList.Add(state.ClientGuid, state);
            state._context.Response.Write(state.ClientGuid.ToString());
        }
    }

    public static void UnregisterClient(String guid)
    {
        wait = false;
        arr = new ArrayList();
        lock (_lock)
        {
            _clientStateList.Remove(guid);
        }
    }

    public static void UpdateClient(AsyncResult state, String guid)
    {
        lock (_lock)
        {
            AsyncResult clientState = null;
            _clientStateList.TryGetValue(guid, out clientState);
            if (clientState != null)
            {
                clientState._context = state._context;
                clientState._state = state._state;
                clientState._callback = state._callback;
            }
        }
    }

    public static void LoadBoard(AsyncResult state, string guid)
    {
        if (wait)
            arr.Add(new Game(guid));
        else
            ((Game)arr[0]).addPlayer(guid);

        Board b = ((Game)arr[0]).getBoardOfPlayer(guid);
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string resultStr = myJavaScriptSerializer.Serialize(b.getBoardArr());
        state._context.Response.Write(resultStr);
        if (wait)
            resultStr = myJavaScriptSerializer.Serialize("wait");
        else
            resultStr = myJavaScriptSerializer.Serialize("play");        
        state._context.Response.Write(resultStr);
        //lock (_lock)
        //{
        //    foreach (AsyncResult clientState in _clientStateList.Values)
        //    {
        //        if (clientState._context.CurrentHandler != null)
        //        {
        //            clientState._context.Response.Write(resultStr);
        //            clientState.CompleteRequest();
        //        }
        //    }
        //}
    }   
}