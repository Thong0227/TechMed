using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechMed.Areas.Admin.Models.Banner
{
    [Table("BannerPosition")]
    public class BannerPosition
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên vị trí.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập mã vị trí.")]
        public string Code { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập mô tả.")]
        public string? Description { get; set; }

        public List<Banner>? Banners { get; set; }
    }
}
