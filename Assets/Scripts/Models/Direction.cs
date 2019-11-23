using System;

namespace Assets.Scripts.Models
{
    [Flags]
    public enum Direction
    {
        Up = 1,
        UpRight = 1 << 1,
        Right = 1 << 2,
        DownRight = 1 << 3,
        Down = 1 << 4,
        DownLeft = 1 << 5,
        Left = 1 << 6,
        UpLeft = 1 << 7
    }
}