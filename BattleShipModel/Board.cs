using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipModel
{
    public class Board
    {
        private static Random rand = new Random();

        public Board()
        {
            BoardArray = new int[Util.BoardSize][];
        }

        public string ID { get; set; }

        public string Struct { get; set; }

        public int numberOfShipsParts { get; set; } //get from database (save in board table maybe?)

        public int[][] BoardArray { get; set; }

    }
}
