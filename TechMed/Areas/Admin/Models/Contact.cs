using System.ComponentModel.DataAnnotations;

namespace TechMed.Areas.Admin.Models
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nội dung là bắt buộc")]
        public string Content { get; set; } = "";
    }
}
