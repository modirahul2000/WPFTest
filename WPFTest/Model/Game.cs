using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest.Model
{
    public class Game
    {
        public int TotalRound { get; set; }

        public bool IsResulted { get; set; }

        public List<Player> Players { get; set; }
        
        public Game(int? players)
        {
            TotalRound = 0;
            IsResulted = false;
            Players = new List<Player>();

            for (int i = 0; i < players; i++)
            {
                //Player ObjPlayer;
                //ObjPlayer = new Player {
                    
                //};

                Players.Add(new Player {
                    ChipCount = 3,
                    PlayerID = i
                });
            }
        }
    }
}
