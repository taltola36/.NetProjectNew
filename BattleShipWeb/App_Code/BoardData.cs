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
    public bool wait = true;
    public int firstToBegin;

    public BoardData(int[][] boardArr, string numberOfSubmarinesLeft, string numberOfAllSubmarines, bool wait, int firstToBegin)
	{
		this.boardArr = boardArr;
        this.numberOfSubmarinesLeft = numberOfSubmarinesLeft;
        this.numberOfAllSubmarines = numberOfAllSubmarines;
        this.wait = wait;
        this.firstToBegin = firstToBegin;
	}
}