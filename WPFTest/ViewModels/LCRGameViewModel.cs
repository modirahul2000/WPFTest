using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Windows.Input;
using WPFTest.Commands;
using System.ComponentModel;
using System.Windows;
using WPFTest.Model;
using System.Text.RegularExpressions;

namespace WPFTest.ViewModels
{
    public class LCRGameViewModel : BindableBase
    {
        private string _GameCount;
        public string GameCount
        {
            get { return _GameCount; }
            set {
                Regex regex = new Regex(@"(?:\d)");
                if (regex.IsMatch(value))
                    SetProperty(ref _GameCount, value);
                else
                    SetProperty(ref _GameCount, null);
            }
        }

        private string _PlayerCount;
        public string PlayerCount
        {
            get { return _PlayerCount; }
            set {
                Regex regex = new Regex(@"(?:\d)");
                if(regex.IsMatch(value))
                    SetProperty(ref _PlayerCount, value); 
                else
                    SetProperty(ref _PlayerCount, null);
            }
        }

        private string _GameResult;
        public string GameResult
        {
            get { return _GameResult; }
            set { SetProperty(ref _GameResult, value); }
        }

        public ICommand ButtonPlayGame { get; set; }

        public PlayGame Games { get; set; }

        public LCRGameViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;
            var myApp = (App)Application.Current;
            ButtonPlayGame = new CustomCommand(ExecuteButtonPlayGame, CanButtonPlayGame);
        }

        private bool CanButtonPlayGame(object parameter)
        {
            try
            {
                if (string.IsNullOrEmpty(GameCount) || string.IsNullOrEmpty(PlayerCount) || Convert.ToInt32(GameCount) < 1 || Convert.ToInt32(PlayerCount) < 3)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        private void ExecuteButtonPlayGame(object parameter)
        {

            try
            {
                Games = new PlayGame(Convert.ToInt32(GameCount), Convert.ToInt32(PlayerCount));

                foreach (var Game in Games.Games)
                {
                    do
                    {
                        if(Game.Players.Where(x=>x.ChipCount>0).Count()>1)
                        {
                            Game.TotalRound++;
                            foreach (var Player in Game.Players)
                            {
                                if (Game.Players.Where(x => x.ChipCount > 0).Count() > 1)
                                {
                                    var ChipCount = Player.ChipCount;
                                    for (int chipcount = 0; chipcount < ChipCount; chipcount++)
                                    {
                                        PlayTurn(Dice.RollDie(), Game, Player);
                                    }
                                }
                            }
                        }

                        if (Game.Players.Where(x => x.ChipCount > 0).Count() == 1)
                            Game.IsResulted = true;
                    }
                    while (!Game.IsResulted);
                }
                var minRound = Games.Games.Min(x => x.TotalRound).ToString();
                var maxRound = Games.Games.Max(x => x.TotalRound).ToString();
                var avgRound= Math.Round(Games.Games.Average(x => x.TotalRound),2).ToString();
                
                GameResult = $"Win With Minimum Round is {minRound} {Environment.NewLine}" +
                    $"Win With Max Round is {maxRound} {Environment.NewLine}" +
                    $"Win With Average Round is {avgRound}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void PlayTurn(char diceresult,Game game,Player player)
        {
            if (diceresult == 'C')
            {
                --player.ChipCount;
            }
            else if (diceresult == 'L')
            {
                --player.ChipCount;
                if (player.PlayerID == 0)
                {
                    game.Players.Last().ChipCount++;
                }
                else
                {
                    game.Players.Where(x => x.PlayerID == player.PlayerID - 1).FirstOrDefault().ChipCount++;
                }
            }
            else if (diceresult == 'R')
            {
                --player.ChipCount;
                if (player.PlayerID == game.Players.Count()-1)
                {
                    game.Players.First().ChipCount++;
                }
                else
                {
                    game.Players.Where(x => x.PlayerID == player.PlayerID + 1).FirstOrDefault().ChipCount++;
                }
            }
        }
    }
}
