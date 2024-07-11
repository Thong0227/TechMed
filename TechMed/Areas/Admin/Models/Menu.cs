using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechMed.Areas.Admin.Models
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; } = "";

        public int? Order { get; set; }

        public string? Link { get; set; }
        public int? Status { get; set; }

        public Guid? ParentId { get; set; }
    }
}
