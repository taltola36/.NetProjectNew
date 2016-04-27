using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

public class Board
{
    private string id;

    public string BoardId
    {
        get { return id; }
        set { id = value; }
    }
    private string structure;

    public string BoardStruct
    {
        get { return structure; }
        set { structure = value; }
    }   

    private int[][] myBoard;

    public Board()
	{
        const int size=10;
        Random rand = new Random();
        myBoard = new int[size][];

        string structer = DBConnection.GetStructerBoard(out id, out structure);
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        int[][] structArr = (int[][])myJavaScriptSerializer.Deserialize(structer, typeof(int[][]));

        
        for (int i = 0; i < size; i++)
        {
            myBoard[i] = new int[size];
            for (int j = 0; j < size; j++)
            {
                myBoard[i][j] = structArr[i][j];
            } 
            // adding note for test commit
        }
	}

    public int[][] getBoardArr()
    {
        return myBoard;
    }
}