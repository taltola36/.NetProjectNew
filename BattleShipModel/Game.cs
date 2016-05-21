using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipModel
{
    public class Game
    {
        public string ID { get; set; }

        public int playTurn { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        //public string MakeMove(int k, int l, string playerId)
        //{
        //    string result = "";
        //    if (Player1 != null)
        //        if (playTurn == 0 && playerId.Equals(Player1.ID))
        //        {
        //            result = Player1.Board.BoardArray[k][l].ToString();
        //            if (result.Equals("1"))
        //                Player1.numberOfHits++;
        //        }
        //    if (Player2 != null)
        //        if (playTurn == 1 && playerId.Equals(Player2.ID))
        //        {
        //            result = Player2.Board.BoardArray[k][l].ToString();
        //            if (result.Equals("1"))
        //                Player2.numberOfHits++;
        //        }
        //    if (result.Equals("0"))
        //        playTurn = 1 - playTurn;
        //    return result;
        //}

        public int NumberOfPlayers
        {
            get
            {
                int count = 0;
                if (Player1 != null)
                {
                    count++;
                }

                if (Player2 != null)
                {
                    count++;
                }
                return count;
            }
        }
    }
}
