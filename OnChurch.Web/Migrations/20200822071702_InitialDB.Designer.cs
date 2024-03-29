﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnChurch.Web.Data;

namespace OnChurch.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200822071702_InitialDB")]
    partial class InitialDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnChurch.Web.Data.Entities.Campus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Campuses");
                });

            modelBuilder.Entity("OnChurch.Web.Data.Entities.Church", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<int?>("ProfessionId");

                    b.Property<int?>("SectionId");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionId");

                    b.HasIndex("SectionId");

                    b.ToTable("Church");
                });

            modelBuilder.Entity("OnChurch.Web.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("ChurchId");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("ChurchId");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("OnChurch.Web.Data.Entities.Profession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Profession");
                });

            modelBuilder.Entity("OnChurch.Web.Data.Entities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CampusId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CampusId");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("OnChurch.Web.Data.Entities.Church", b =>
                {
                    b.HasOne("OnChurch.Web.Data.Entities.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("ProfessionId");

                    b.HasOne("OnChurch.Web.Data.Entities.Section")
                        .WithMany("Churches")
                        .HasForeignKey("SectionId");
                });

            modelBuilder.Entity("OnChurch.Web.Data.Entities.User", b =>
                {
                    b.HasOne("OnChurch.Web.Data.Entities.Church")
                        .WithMany("Members")
                        .HasForeignKey("ChurchId");
                });

            modelBuilder.Entity("OnChurch.Web.Data.Entities.Section", b =>
                {
                    b.HasOne("OnChurch.Web.Data.Entities.Campus")
                        .WithMany("Sections")
                        .HasForeignKey("CampusId");
                });
#pragma warning restore 612, 618
        }
    }
}
