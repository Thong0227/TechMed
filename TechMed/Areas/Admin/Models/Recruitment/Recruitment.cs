using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechMed.Areas.Admin.Models.Recruitment
{
    [Table("Recruitment")]
    public class Recruitment
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên công việc là bắt buộc")]
        public string Name { get; set; } = "";

        [StringLength(50, ErrorMessage = "Độ dài không quá 50 kí tự")]
        public string? Salary { get; set; }

        [StringLength(500, ErrorMessage = "Description must not be over 500 characters")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Nội dung bắt buộc")]
        public string Content { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [StringLength(50, ErrorMessage = "Số lượng không quá 50 kí tự")]
        public string? Quantity { get; set; }
        public string? Image { get; set; }
		
		public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
		
		public DateTime StartDate { get; set; }
		
		public DateTime? EndDate { get; set; }

        public int Status { get; set; }

		public int? Order { get; set; }
		public int? Type { get; set; }
		public Guid? RecruitmentCateId { get; set; }
        public RecruitmentCate? RecruitmentCate { get; set; }

        public ICollection<RecruitmentTag>? RecruitmentTags { get; set; }

    }
}
