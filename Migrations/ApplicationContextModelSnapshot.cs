﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication121024_Films.Data;

#nullable disable

namespace WebApplication121024_Films.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication121024_Films.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TvShowId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TvShowId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("WebApplication121024_Films.Models.TvShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImdbUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TvShow");
                });

            modelBuilder.Entity("WebApplication121024_Films.Models.Comment", b =>
                {
                    b.HasOne("WebApplication121024_Films.Models.TvShow", "TvShow")
                        .WithMany("Comments")
                        .HasForeignKey("TvShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TvShow");
                });

            modelBuilder.Entity("WebApplication121024_Films.Models.TvShow", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
