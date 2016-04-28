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
    private static bool wait = false, registered = false; // reg true is the first player
    private static ArrayList arr = new ArrayList();

    public static void RegisterClient(AsyncResult state)
    {
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string resultStr;
        registered = !registered; 
        lock (_lock)
        {
            state.ClientGuid = Guid.NewGuid().ToString();
            _clientStateList.Add(state.ClientGuid, state);
            state._context.Response.Write(state.ClientGuid.ToString());
        }

        if (registered)
            resultStr = myJavaScriptSerializer.Serialize("wait");
        else
        {
            resultStr = myJavaScriptSerializer.Serialize("play");
            updateWaitingClient(state, resultStr);
        }
        state._context.Response.Write(resultStr);
    }

    public static void updateWaitingClient(AsyncResult state, string resultStr)
    {
        lock (_lock)
        {
            foreach (AsyncResult clientState in _clientStateList.Values)
            {
                if (clientState._context.CurrentHandler != null && !state.ClientGuid.Equals(clientState.ClientGuid))
                {
                    clientState._context.Response.Write(resultStr);
                    clientState.CompleteRequest();
                }
            }
        }
    }

    public static void UnregisterClient(AsyncResult state, String guid)
    {
        string resultStr;
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();

        //wait = false;
        //registered = false;
        //arr = new ArrayList();
        //for (int i = 0; i < arr.Count; i++)
        //{
        //    if (((Game)arr[i]).getPlayer(1) != null && ((Game)arr[i]).getPlayer(1).getGuid().Equals(guid))
        //    {
        //        ((Game) arr[i]).removePlayer(1);
        //    }
        //    if (((Game)arr[i]).getPlayer(2) != null && ((Game)arr[i]).getPlayer(2).getGuid().Equals(guid))
        //    {
        //        ((Game) arr[i]).removePlayer(2);
        //    }
        //    resultStr = myJavaScriptSerializer.Serialize("The other player left. Would you like to play again?");
        //    updateWaitingClient(state, resultStr);
        //}
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
        //if (wait)
        //    arr.Add(new Game(guid));
        //else
        //    ((Game)arr[0]).addPlayer(guid);

        //Board b = ((Game)arr[0]).getBoardOfPlayer(guid);
        //JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        //string resultStr = myJavaScriptSerializer.Serialize(b.getBoardArr());
        //state._context.Response.Write(resultStr);
        //if (wait)
        //    resultStr = myJavaScriptSerializer.Serialize("wait");
        //else
        //    resultStr = myJavaScriptSerializer.Serialize("play");        
        //state._context.Response.Write(resultStr);

        Board b;
        wait = !wait;
        //if (((Game)arr[0]).)
        if (arr.Count != 0 && ((Game)arr[0]).getNumberofPlayers() == 2)
            b = ((Game)arr[0]).createNewBoard(guid);
        else
        {
            if (wait)
                arr.Add(new Game(guid));
            else
                ((Game)arr[0]).addPlayer(guid);
            b = ((Game)arr[0]).getBoardOfPlayer(guid);
        }

        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string resultStr = myJavaScriptSerializer.Serialize(b.getBoardArr());

        state._context.Response.Write(resultStr);
        updateWaitingClient(state, resultStr);

    }   
}