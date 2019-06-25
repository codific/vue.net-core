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
    }
}
