﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UrlShortener.Data;

#nullable disable

namespace UrlShortener.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UrlShortener.Models.Entities.ShortUrl", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecretCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SecretCode")
                        .IsUnique();

                    b.HasIndex("ShortCode")
                        .IsUnique();

                    b.ToTable("ShortUrls");
                });

            modelBuilder.Entity("UrlShortener.Models.Entities.UrlAccessLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AccessedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ShortUrlId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ShortUrlId");

                    b.ToTable("UrlAccessLogs");
                });

            modelBuilder.Entity("UrlShortener.Models.Entities.UrlAccessLog", b =>
                {
                    b.HasOne("UrlShortener.Models.Entities.ShortUrl", "ShortUrl")
                        .WithMany("AccessLogs")
                        .HasForeignKey("ShortUrlId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShortUrl");
                });

            modelBuilder.Entity("UrlShortener.Models.Entities.ShortUrl", b =>
                {
                    b.Navigation("AccessLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
