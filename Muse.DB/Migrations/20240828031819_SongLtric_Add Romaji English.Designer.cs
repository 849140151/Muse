﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Muse.DB.Configuration;

#nullable disable

namespace Muse.DB.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240828031819_SongLtric_Add Romaji English")]
    partial class SongLtric_AddRomajiEnglish
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.7.24405.3");

            modelBuilder.Entity("Muse.DB.Model.SongBasic", b =>
                {
                    b.Property<int>("SongBasicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Album")
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("Duration")
                        .HasColumnType("REAL");

                    b.Property<string>("Performers")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("SongBasicId");

                    b.ToTable("SongBasic");
                });

            modelBuilder.Entity("Muse.DB.Model.SongDetail", b =>
                {
                    b.Property<int>("SongDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BilibiliUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("LocalUrl")
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

            modelBuilder.Entity("Muse.DB.Model.SongLyric", b =>
                {
                    b.Property<int>("SongLyricId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Chinese")
                        .HasColumnType("TEXT");

                    b.Property<string>("English")
                        .HasColumnType("TEXT");

                    b.Property<string>("Kanji")
                        .HasColumnType("TEXT");

                    b.Property<double>("LyricTimeStamp")
                        .HasColumnType("REAL");

                    b.Property<string>("RoMaJi")
                        .HasColumnType("TEXT");

                    b.Property<int>("SongBasicId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SongLyricId");

                    b.HasIndex("SongBasicId");

                    b.ToTable("SongLyrics");
                });

            modelBuilder.Entity("Muse.DB.Model.SongDetail", b =>
                {
                    b.HasOne("Muse.DB.Model.SongBasic", "SongBasic")
                        .WithOne("SongDetail")
                        .HasForeignKey("Muse.DB.Model.SongDetail", "SongBasicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SongBasic");
                });

            modelBuilder.Entity("Muse.DB.Model.SongLyric", b =>
                {
                    b.HasOne("Muse.DB.Model.SongBasic", "SongBasic")
                        .WithMany()
                        .HasForeignKey("SongBasicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SongBasic");
                });

            modelBuilder.Entity("Muse.DB.Model.SongBasic", b =>
                {
                    b.Navigation("SongDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
