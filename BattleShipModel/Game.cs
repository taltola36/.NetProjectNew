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

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

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
