using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest.Model
{
    public static class Dice
    {
        public static List<char> Die = new List<char>();
        static Dice()
        {
            Die.Add('L');
            Die.Add('C');
            Die.Add('R');
            Die.Add('.');
            Die.Add('.');
            Die.Add('.');
        }

        public static char RollDie()
        {
            Random rand = new Random();
            return Die[rand.Next(0, 5)];
        }
    }
}
