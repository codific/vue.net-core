using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public enum MovePosition
    {
        TopLeft = 1,
        TopCenter = 2,
        TopRight = 3,
        MiddleLeft = 4,
        MiddleCenter = 5,
        MiddleRight = 6,
        BottomLeft = 7,
        BottomCenter = 8,
        BottomRight = 9
    }

    public enum WinningCombination
    {
        TopRow = 123, 
        MiddleRow = 456,
        BottomRow = 789,
        LeftColumn = 147,
        CenterColumn = 258,
        RightColumn = 369,
        MainDiagonal = 159,
        SecondaryDiagonal = 357
    }
}
