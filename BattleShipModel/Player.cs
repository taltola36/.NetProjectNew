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

        public Board Board { get; set; }

        /// <summary>
        /// The GUID ID for the player
        /// </summary>
        public string ID { get; set; }

    }
}
