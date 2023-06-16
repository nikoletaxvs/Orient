﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orient.Data;

#nullable disable

namespace Orient.Migrations
{
    [DbContext(typeof(ApplicationDbContect))]
    [Migration("20230615110456_questionUpdated")]
    partial class questionUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Orient.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("EducationLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Orient.Models.AccountStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("UXAttempts")
                        .HasColumnType("int");

                    b.Property<int>("UXCompletions")
                        .HasColumnType("int");

                    b.Property<int>("UXMeanScore")
                        .HasColumnType("int");

                    b.Property<int>("dataScienceCompletions")
                        .HasColumnType("int");

                    b.Property<int>("dataSciencegMeanScore")
                        .HasColumnType("int");

                    b.Property<int>("dataScienceingAttempts")
                        .HasColumnType("int");

                    b.Property<int>("gameAttempts")
                        .HasColumnType("int");

                    b.Property<int>("gameCompletions")
                        .HasColumnType("int");

                    b.Property<int>("gameMeanScore")
                        .HasColumnType("int");

                    b.Property<int>("loginCount")
                        .HasColumnType("int");

                    b.Property<int>("msAttempts")
                        .HasColumnType("int");

                    b.Property<int>("msCompletions")
                        .HasColumnType("int");

                    b.Property<int>("msMeanScore")
                        .HasColumnType("int");

                    b.Property<int>("softwareEngineeringAttempts")
                        .HasColumnType("int");

                    b.Property<int>("softwareEngineeringCompletions")
                        .HasColumnType("int");

                    b.Property<int>("softwareEnginneringMeanScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AccountStatistics");
                });

            modelBuilder.Entity("Orient.Models.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnswerId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Correct")
                        .HasColumnType("bit");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("AnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Orient.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Orient.Models.unit1_question", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("answear1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("answear2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("answear3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("answear4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("correctAnswear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("unit1_Questions");
                });

            modelBuilder.Entity("Orient.Models.Answer", b =>
                {
                    b.HasOne("Orient.Models.Question", "Questions")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Orient.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}