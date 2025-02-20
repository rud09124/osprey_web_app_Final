using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using osprey_web_app.Areas.Identity.Data;

namespace osprey_web_app.Models
{
    public class UserWorkshop
    {
        [Key]
        public int Id { get; set; } // ✅ Primary Key

        [Required]
        public string UserId { get; set; } // ✅ Foreign Key to User

        [ForeignKey("UserId")]
        public osprey_web_appUser User { get; set; } // ✅ Reference to User

        [Required]
        public int WorkshopId { get; set; } // ✅ Foreign Key to Workshop

        [ForeignKey("WorkshopId")]
        public Workshop Workshop { get; set; } // ✅ Reference to Workshop

        [Required]
        public string WhatsAppNumber { get; set; } // ✅ Store WhatsApp number
    }
}
