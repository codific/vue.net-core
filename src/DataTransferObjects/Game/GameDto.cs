using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Game
{
    public class GameDto
    {
        public Guid Id { get; set; }

        public bool IsFinished { get; set; }

        public bool? IsPlayerWin { get; set; }

        public int? WinningCombination { get; set; }

        public string PlayerSign { get; set; }

        public string MachineSign
        {
            get
            {
                return PlayerSign.Equals("X") ? "O" : "X";
            }
        }
    }
}
