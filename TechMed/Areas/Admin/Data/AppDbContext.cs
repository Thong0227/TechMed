using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Models.News;
using TechMed.Areas.Admin.Models.Recruitment;
using TechMed.Areas.Admin.Models.Banner;
using Microsoft.Extensions.Hosting;
using TechMed.Areas.Admin.Models;

namespace TechMed.Areas.Admin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<TechMed.Areas.Admin.Models.News.News>? News { get; set; }
        public DbSet<TechMed.Areas.Admin.Models.News.NewsCategory>? NewsCategory { get; set; }
        
        public DbSet<TechMed.Areas.Admin.Models.Recruitment.RecruitmentCate>? RecruitmentCate { get; set; }
        
        public DbSet<TechMed.Areas.Admin.Models.Menu>? Menu { get; set; }
        public DbSet<TechMed.Areas.Admin.Models.Banner.Banner>? Banner { get; set; }
        public DbSet<TechMed.Areas.Admin.Models.Banner.BannerPosition>? BannerPosition { get; set; }
        public DbSet<TechMed.Areas.Admin.Models.Recruitment.Recruitment>? Recruitments { get; set; }
        public DbSet<TechMed.Areas.Admin.Models.Recruitment.Tag>? Tags { get; set; }
        public DbSet<TechMed.Areas.Admin.Models.Recruitment.RecruitmentTag>? RecruitmentTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecruitmentTag>()
                .HasKey(rt => new { rt.RecruitmentId, rt.TagId });

            modelBuilder.Entity<RecruitmentTag>()
                .HasOne(rt => rt.Recruitment)
                .WithMany(r => r.RecruitmentTags)
                .HasForeignKey(rt => rt.RecruitmentId);

            modelBuilder.Entity<RecruitmentTag>()
                .HasOne(rt => rt.Tag)
                .WithMany(t => t.RecruitmentTags)
                .HasForeignKey(rt => rt.TagId);
        }

        public DbSet<TechMed.Areas.Admin.Models.Contact>? Contact { get; set; }
    }
}
