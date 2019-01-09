﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NicolePhramd.Models;

namespace NicolePhramd.Migrations
{
    [DbContext(typeof(PhramdContext))]
    partial class PhramdContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NicolePhramd.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("signupdate");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("NicolePhramd.Models.WeatherDB", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("city")
                        .IsRequired();

                    b.Property<string>("country")
                        .IsRequired();

                    b.Property<string>("status")
                        .IsRequired();

                    b.Property<string>("unit")
                        .IsRequired();

                    b.Property<int>("userId");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Weather");
                });

            modelBuilder.Entity("NicolePhramd.Models.WeatherDB", b =>
                {
                    b.HasOne("NicolePhramd.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
