using System;
using System.Collections.Generic;
using System.Text;

namespace Blake.Shared.Entities.Snake
{
    public class SnakeDifficulty
    {
        public int StartingSpeedMs { get; set; }

        public int[] BoardDimensions { get; set; }

        public int IncrementStep { get; set; }
    }
}
