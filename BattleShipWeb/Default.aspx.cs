using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private const int size = 10, buttonSize = 30;
    private int linePosLeft = buttonSize * 5, linePosRight = buttonSize * (size+10), top;
    private Button[,] leftBoard = new Button[size,size];
    private Button[,] rightBoard = new Button[size,size];
    private string[] letterArr = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setLabel("Battleship Game", "battleshipGameLabel", 0, 5);
            setLabel("Player Name", "playerNameLabel", 0, 980);
            setLabel("Your battleships", "YourBattleshipsLabel", (size + 4) * buttonSize, (size / 2 + 3) * buttonSize);
            setLabel("Enemy battleships", "enemyBattleshipsLabel", (size + 4) * buttonSize, (size + size + 3) * buttonSize);
            setBoard(leftBoard, linePosLeft, "disabled");
            setBoard(rightBoard, linePosRight, "");
            setButton("New Game", buttonSize, 5);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                leftBoard[i,j] = new Button();
                rightBoard[i,j] = new Button();
                leftBoard[i, j].ID = (i*size+j + size * size).ToString();
                rightBoard[i,j].ID = (i*size+j).ToString();
                form1.Controls.Add(leftBoard[i,j]);
                form1.Controls.Add(rightBoard[i,j]);
            }
        }
    }

    protected void setButton(string text, int top, int left)
    {
        Button myButton = new Button();
        myButton.Text = text;
        myButton.Style.Add("width", "5");
        myButton.Style.Add("height", "5");
        myButton.Style.Add("font-style", "david");
        myButton.Style.Add("position", "absolute");
        myButton.Style.Add("top", top.ToString() + "px");
        myButton.Style.Add("left", left.ToString() + "px");
        form1.Controls.Add(myButton);
        myButton.Attributes.Add("onclick", "newGameClick();return false");
    }

    protected void setLabel(string text, string id, int top, int left)
    {
        Label myLabel = new Label();
        myLabel.Text = text;
        myLabel.ID = id;
        myLabel.Style.Add("width", "5");
        myLabel.Style.Add("height", "5");
        myLabel.Style.Add("font-style", "david");
        myLabel.Style.Add("position", "absolute");
        myLabel.Style.Add("top", top.ToString() + "px");
        myLabel.Style.Add("left", left.ToString() + "px");
        form1.Controls.Add(myLabel);
    }

    protected void setBoard(Button[,] buttonArr, int linePos, string disabled)
    {
        top = buttonSize*3;
        for (int i = 0; i < size; i++)
        {
            setLabel((i + 1).ToString(),"label", top, linePos-buttonSize);
            for (int j = 0; j < size; j++)
            {
                if (buttonArr == leftBoard)
                    buttonArr[i, j].Attributes.Add("disabled", "disabled");
                setLabel(letterArr[j], "label", buttonSize*2, linePos + 10);
                buttonArr[i, j].Width = buttonArr[i, j].Height = buttonSize;
                buttonArr[i, j].Style.Add("position", "absolute");
                buttonArr[i, j].Style.Add("top", top.ToString() + "px");
                buttonArr[i, j].Style.Add("left", linePos.ToString() + "px");
                buttonArr[i, j].Attributes.Add("onclick", "buttonClick(this);return false");
                linePos += buttonSize;
            }

            if (buttonArr == leftBoard)
                linePos = buttonSize*5;
            else
                linePos = buttonSize * (size + 10);
            top += buttonSize;
        }
    }
}