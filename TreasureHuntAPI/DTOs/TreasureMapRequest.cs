﻿namespace TreasureHuntAPI.DTOs
{
    public class TreasureMapRequest
    {
        public int N { get; set; }
        public int M { get; set; }
        public int P { get; set; }
        public int[][] Matrix { get; set; } = null!;
    }
}