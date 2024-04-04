using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechMed.Areas.Admin.Models.News
{
    [Table("News")]
    public class News
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Title must be between 6 and 100 characters")]
        public string Title { get; set; } = "";

        [StringLength(500, ErrorMessage = "Description must not be over 500 characters")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = "";

        public int Status { get; set; }

        public string? Image { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? NewsCategoryId { get; set; }

        public NewsCategory? NewsCategory { get; set; }

    }
}
