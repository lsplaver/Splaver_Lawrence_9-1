using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigGame.Models
{
    public class Pig
    {
        public int PlayerOneTotalScore { get; set; }

        public int PlayerTwoTotalScore { get; set; }

        public int CurrentTurnScore { get; set; }

        public int CurrentPlayer { get; set; }
    }
}
