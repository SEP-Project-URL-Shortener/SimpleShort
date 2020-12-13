﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleShort.Data;

namespace SimpleShort.Migrations
{
    [DbContext(typeof(ShortenedUrlContext))]
    partial class ShortenedUrlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("SimpleShort.Entities.Logging.LogEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Information")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Occurred")
                        .HasColumnType("datetime2");

                    b.Property<string>("Request")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("SimpleShort.Entities.ShortenedUrl", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Created")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Expiration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUsed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfUses")
                        .HasColumnType("int");

                    b.Property<string>("OriginalUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShortenedUrls");
                });
#pragma warning restore 612, 618
        }
    }
}
