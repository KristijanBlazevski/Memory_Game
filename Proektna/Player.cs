using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proektna
{
    public class Player
    {
        public string Name { get; set; }
        public int score { get; set; } = 0;
        public int timeLeft { get; set; } = 60; 
        public bool UsedJoker { get; set; } = false;
        public bool UsedReset { get; set; } = false;

        bool jokerUsed;

        public Player(string name)
        {
            Name = name;
           
        }

    }
}
