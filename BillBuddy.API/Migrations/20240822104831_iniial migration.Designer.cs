﻿// <auto-generated />
using System;
using BillBuddy.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BillBuddy.API.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240822104831_iniial migration")]
    partial class iniialmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BillBuddy.API.Models.SplitTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DueDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PaidBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ParticipantPublicIdentifiers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PublicIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SplitDateTIme")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDateTIme")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SplitTransactions");
                });

            modelBuilder.Entity("BillBuddy.API.Models.SplitTransactionParticipant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BalanceAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("LastPaidDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ParticipantUserId")
                        .HasColumnType("int");

                    b.Property<Guid>("PublicIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SettlementStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("SplitAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("SplitTransactionParticipantIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantUserId");

                    b.ToTable("SplitTransactionParticipants");
                });

            modelBuilder.Entity("BillBuddy.API.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Auth0Identifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PublicIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BillBuddy.API.Models.SplitTransactionParticipant", b =>
                {
                    b.HasOne("BillBuddy.API.Models.User", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");
                });
#pragma warning restore 612, 618
        }
    }
}
