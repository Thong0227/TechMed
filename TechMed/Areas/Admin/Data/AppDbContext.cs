using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Models.News;
using TechMed.Areas.Admin.Models.Recruitment;
using TechMed.Areas.Admin.Models.Banner;
using Microsoft.Extensions.Hosting;

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
        public DbSet<TechMed.Areas.Admin.Models.Recruitment.Recruitment>? Recruitment { get; set; }
        public DbSet<TechMed.Areas.Admin.Models.Recruitment.Tag>? Tag { get; set; }
        public DbSet<TechMed.Areas.Admin.Models.Recruitment.RecruitmentTag>? RecruitmentTag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecruitmentTag>(entity =>
            {
                entity.HasKey(bp => new {bp.RecruitmentId,bp.TagId});

                entity.HasOne(bp => bp.Recruitment)
                .WithMany(b => b.RecruitmentTags)
                .HasForeignKey(bp => bp.RecruitmentId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(bp => bp.Tag)
                .WithMany(b => b.RecruitmentTags)
                .HasForeignKey(bp => bp.TagId)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
