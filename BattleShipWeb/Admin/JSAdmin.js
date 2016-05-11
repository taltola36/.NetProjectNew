var checkBox = {};
var xmlHttp_addBoard;



function newXmlHttpReq() {
    try {
        xmlHttp_addBoard = new ActiveXObject("Microsoft.XMLHTTP");
        //xmlHttp_Process = new ActiveXObject("Microsoft.XMLHTTP");
    }
    catch (e) {
        try {
            xmlHttp_addBoard = new XMLHttpRequest();
            //xmlHttp_Process = new XMLHttpRequest();
        }
        catch (e) {
            alert("Error");
        }
    }
}

function buttonAddClick() {
    for (var i = 1; i <= 100; i++) {
        checkBox[i] = document.getElementById("CheckBox" + i);
    }
    var board = "[[";

    for (var j = 1; j <= 100; j++)
    {
        if (checkBox[j].checked)
        {
            if (j == 100)
            {
                board += "1";
                break;
            }
            if (j % 10 == 0)
            {
                board += "1],[";
            }
            else
            {
                board += "1,";
            }
        }
        else if (!checkBox[j].checked)
        {
            if (j == 100)
            {
                board += "0";
                break;
            }
            if (j % 10 == 0)
            {
                board += "0],[";
            }
            else
            {
                board += "0,";
            }
        }
    }

    board += "]]";

    var url = "AdminHandler.ashx?cmd=addBoard&stringBoard=" + board;
    //alert(xmlHttp_Process.readyState);
    xmlHttp_addBoard.open("POST", url, true);
    xmlHttp_addBoard.onreadystatechange = onRequestAddBoard;
    xmlHttp_addBoard.send();
}

function onRequestAddBoard() {
    if (xmlHttp_addBoard.readyState == 4) {
        var myJSON_Text = xmlHttp_addBoard.responseText;
        alert(eval(myJSON_Text));
        resetBoard();
    }
}

function resetBoard() {
    for (var i = 1; i <= 100; i++) {
        checkBox[i].checked = false;
    }
}

window.onload = newXmlHttpReq;