﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechMed.Areas.Admin.Data;

#nullable disable

namespace TechMed.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240422070042_updateLinkOrder")]
    partial class updateLinkOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Banner.Banner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BannerPositionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BannerPositionId");

                    b.ToTable("Banner");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Banner.BannerPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BannerPosition");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.News.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("NewsCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NewsCategoryId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.News.NewsCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("NewsCategory");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Recruitment.Recruitment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Quantity")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("RecruitmentCateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Salary")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecruitmentCateId");

                    b.ToTable("Recruitment");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Recruitment.RecruitmentCate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("RecruitmentCate");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Recruitment.RecruitmentTag", b =>
                {
                    b.Property<Guid>("RecruitmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RecruitmentId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("RecruitmentTags");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Recruitment.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Banner.Banner", b =>
                {
                    b.HasOne("TechMed.Areas.Admin.Models.Banner.BannerPosition", "BannerPosition")
                        .WithMany("Banners")
                        .HasForeignKey("BannerPositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BannerPosition");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.News.News", b =>
                {
                    b.HasOne("TechMed.Areas.Admin.Models.News.NewsCategory", "NewsCategory")
                        .WithMany("News")
                        .HasForeignKey("NewsCategoryId");

                    b.Navigation("NewsCategory");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Recruitment.Recruitment", b =>
                {
                    b.HasOne("TechMed.Areas.Admin.Models.Recruitment.RecruitmentCate", "RecruitmentCate")
                        .WithMany("Recruitments")
                        .HasForeignKey("RecruitmentCateId");

                    b.Navigation("RecruitmentCate");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Recruitment.RecruitmentTag", b =>
                {
                    b.HasOne("TechMed.Areas.Admin.Models.Recruitment.Recruitment", "Recruitment")
                        .WithMany("RecruitmentTags")
                        .HasForeignKey("RecruitmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechMed.Areas.Admin.Models.Recruitment.Tag", "Tag")
                        .WithMany("RecruitmentTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recruitment");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Banner.BannerPosition", b =>
                {
                    b.Navigation("Banners");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.News.NewsCategory", b =>
                {
                    b.Navigation("News");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Recruitment.Recruitment", b =>
                {
                    b.Navigation("RecruitmentTags");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Recruitment.RecruitmentCate", b =>
                {
                    b.Navigation("Recruitments");
                });

            modelBuilder.Entity("TechMed.Areas.Admin.Models.Recruitment.Tag", b =>
                {
                    b.Navigation("RecruitmentTags");
                });
#pragma warning restore 612, 618
        }
    }
}
