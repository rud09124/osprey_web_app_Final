using System.ComponentModel.DataAnnotations;

namespace osprey_web_app.Models
{
    public class Mentor
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Specialization { get; set; }

        public string ImageUrl { get; set; } // Optional: URL to mentor's image
    }
}
