using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipModel
{
    public class Player
    {
        public Player(string guid)
        {
            this.ID = guid;
        }

        public int numberOfHits { get; set; }

        public int numberOfSubmarines { get; set; }

        public Board Board { get; set; }

        public string userName { get; set; }

        public string ID { get; set; }

    }
}
