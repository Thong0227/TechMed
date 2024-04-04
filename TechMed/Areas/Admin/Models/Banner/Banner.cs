using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechMed.Areas.Admin.Models.Banner
{
    [Table("Banner")]
    public class Banner
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Tên banner không quá 500 kí tự")]
        public string Name { get; set; } = "";

        public string? ImageUrl { get; set; }

        public int Status { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày hiển thị.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn vị trí banner hiển thị.")]
        public Guid BannerPositionId { get; set; }
        public BannerPosition BannerPosition { get; set; }
    }
}
