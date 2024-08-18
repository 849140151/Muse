﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Muse.DB.Configuration;

#nullable disable

namespace Muse.UI.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240817010138_add foreignkey")]
    partial class addforeignkey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.7.24405.3");

            modelBuilder.Entity("Muse.UI.Model.SongBasic", b =>
                {
                    b.Property<int>("SongBasicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Album")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Performers")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("SongBasicId");

                    b.ToTable("SongBasic");
                });

            modelBuilder.Entity("Muse.UI.Model.SongDetail", b =>
                {
                    b.Property<int>("SongDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BilibiliUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("LocalUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Lyrics")
                        .HasColumnType("TEXT");

                    b.Property<string>("OtherUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("SongBasicId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("YoutubeUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("SongDetailId");

                    b.HasIndex("SongBasicId")
                        .IsUnique();

                    b.ToTable("SongDetail");
                });

            modelBuilder.Entity("Muse.UI.Model.SongDetail", b =>
                {
                    b.HasOne("Muse.UI.Model.SongBasic", "SongBasic")
                        .WithOne("SongDetail")
                        .HasForeignKey("Muse.UI.Model.SongDetail", "SongBasicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SongBasic");
                });

            modelBuilder.Entity("Muse.UI.Model.SongBasic", b =>
                {
                    b.Navigation("SongDetail");
                });
#pragma warning restore 612, 618
        }
    }
}