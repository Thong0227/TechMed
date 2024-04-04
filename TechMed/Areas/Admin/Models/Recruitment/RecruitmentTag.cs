namespace TechMed.Areas.Admin.Models.Recruitment
{
    public class RecruitmentTag
    {
        public Recruitment Recruitment { get; set; }

        public Guid RecruitmentId { get; set;}

        public Tag Tag { get; set; }

        public Guid TagId { get; set; }
    }
}
