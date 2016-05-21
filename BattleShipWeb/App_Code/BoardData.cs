using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class BoardData
{
    public int[][] boardArr;
    public string numberOfSubmarinesLeft;
    public string numberOfAllSubmarines;

    public BoardData(int[][] boardArr, string numberOfSubmarinesLeft, string numberOfAllSubmarines)
	{
		this.boardArr = boardArr;
        this.numberOfSubmarinesLeft = numberOfSubmarinesLeft;
        this.numberOfAllSubmarines = numberOfAllSubmarines;
	}
}