using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataTransferObjects.Game
{
    public class MoveDto
    {
        [Required]
        public int Order { get; set; }

        [Required]
        [Range(1,9)]
        public int Position { get; set; }

        [Required]
        public bool IsPlayer { get; set; }

        public int Row
        {
            get
            {
                return (int)Math.Ceiling((double)Position / 3);
            }
        }

        public int Column
        {
            get
            {
                return (Position - 1) % 3 + 1;
            }
        }
    }
}
