﻿// <auto-generated />
using System;
using Crowdfund.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Crowdfund.Core.Migrations
{
    [DbContext(typeof(CrowdfundDbContext))]
    [Migration("20200211192005_m2m_8")]
    partial class m2m_8
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Crowdfund.Core.Model.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProgressId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgressId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Buyer");
                });

            modelBuilder.Entity("Crowdfund.Core.Model.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("Crowdfund.Core.Model.Progress", b =>
                {
                    b.Property<int>("ProgressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Contributions")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Goal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("NumberOfContributions")
                        .HasColumnType("int");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("ProgressId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Progress");
                });

            modelBuilder.Entity("Crowdfund.Core.Model.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectCategory")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Crowdfund.Core.Model.ProjectBuyer", b =>
                {
                    b.Property<int>("BuyerId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("BuyerId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectBuyer");
                });

            modelBuilder.Entity("Crowdfund.Core.Model.Reward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Reward");
                });

            modelBuilder.Entity("Crowdfund.Core.Model.Buyer", b =>
                {
                    b.HasOne("Crowdfund.Core.Model.Progress", null)
                        .WithMany("Buyer")
                        .HasForeignKey("ProgressId");

                    b.HasOne("Crowdfund.Core.Model.Project", null)
                        .WithMany("Buyers")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Crowdfund.Core.Model.Progress", b =>
                {
                    b.HasOne("Crowdfund.Core.Model.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("Crowdfund.Core.Model.Project", b =>
                {
                    b.HasOne("Crowdfund.Core.Model.Owner", null)
                        .WithMany("Projects")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("Crowdfund.Core.Model.ProjectBuyer", b =>
                {
                    b.HasOne("Crowdfund.Core.Model.Buyer", "Buyer")
                        .WithMany("Projects")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Crowdfund.Core.Model.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Crowdfund.Core.Model.Reward", b =>
                {
                    b.HasOne("Crowdfund.Core.Model.Buyer", null)
                        .WithMany("Rewards")
                        .HasForeignKey("BuyerId");

                    b.HasOne("Crowdfund.Core.Model.Owner", null)
                        .WithMany("Rewards")
                        .HasForeignKey("OwnerId");
                });
#pragma warning restore 612, 618
        }
    }
}
