using System.ComponentModel.DataAnnotations;

namespace Blake.Shared.Models.DbModels
{
    public class Score
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[A-za-z0-9]+$")]
        public string PlayerName { get; set; }

        public int Points { get; set; }
    }
}
