using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace osprey_web_app.Models
{
    public class Workshop
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = "New Workshop";

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; } = DateTime.Now.AddDays(1);

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = "Workshop description";

        [NotMapped]
        public string Status => Date.ToUniversalTime() < DateTime.UtcNow ? "past" : "upcoming";
    }
}