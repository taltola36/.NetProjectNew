using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class locResult
{
    public string indexes, boardName, isHit, submarinesLeft;

    public locResult(string indexes, string boardName, string isHit, string submarinesLeft)
    {
        this.indexes = indexes;
        this.boardName = boardName;
        this.isHit = isHit;
        this.submarinesLeft = submarinesLeft;
    }
}