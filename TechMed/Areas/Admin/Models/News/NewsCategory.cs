using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechMed.Areas.Admin.Models.News
{
    [Table("NewsCategory")]
    public class NewsCategory
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; } = "";

        // Danh sách tin tức thuộc loại này
        public List<News>? News { get; set; }
    }
}
