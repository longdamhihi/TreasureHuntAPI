using System.ComponentModel.DataAnnotations;

namespace TreasureHuntAPI.Models
{
    public class TreasureMap
    {
        [Key]
        public int Id { get; set; }

        public int N { get; set; }
        public int M { get; set; }
        public int P { get; set; }
        public string MatrixJson { get; set; } = null!;
        public double MinimalFuel { get; set; }
    }
}