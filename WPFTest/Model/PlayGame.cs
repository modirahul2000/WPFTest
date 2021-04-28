using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest.Model
{
    public class PlayGame
    {
        public int? TotalGames { get; set; }

        public int? TotalPlayers { get; set; }

        public List<Game> Games { get; set; }


        public PlayGame()
        {

        }

        public PlayGame(int? totalGames, int? totalPlayers)
        {
            TotalGames = totalGames;
            TotalPlayers = totalPlayers;
            Games = new List<Game>();

            for (int i = 0; i < TotalGames; i++)
            {
                Games.Add(new Game(totalPlayers));
            }
        }

        public bool IsPlayble()
        {
            if (Games.Where(x => x.IsResulted == false).Count() > 0)
                return true;
            else
                return false;
        }
    }
}
