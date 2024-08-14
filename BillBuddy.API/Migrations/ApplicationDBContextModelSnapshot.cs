﻿// <auto-generated />
using System;
using BillBuddy.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BillBuddy.API.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BillBuddy.API.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("BillBuddy.API.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ContributionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int");

                    b.Property<int>("UserDetailsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.HasIndex("UserDetailsId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("BillBuddy.API.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTIme")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaidById")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PaidById");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BillBuddy.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BillBuddy.API.Models.Participant", b =>
                {
                    b.HasOne("BillBuddy.API.Models.Transaction", null)
                        .WithMany("participants")
                        .HasForeignKey("TransactionId");

                    b.HasOne("BillBuddy.API.Models.User", "UserDetails")
                        .WithMany()
                        .HasForeignKey("UserDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("BillBuddy.API.Models.Transaction", b =>
                {
                    b.HasOne("BillBuddy.API.Models.User", "PaidBy")
                        .WithMany("Transactions")
                        .HasForeignKey("PaidById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaidBy");
                });

            modelBuilder.Entity("BillBuddy.API.Models.User", b =>
                {
                    b.HasOne("BillBuddy.API.Models.Group", null)
                        .WithMany("Participants")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("BillBuddy.API.Models.Group", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("BillBuddy.API.Models.Transaction", b =>
                {
                    b.Navigation("participants");
                });

            modelBuilder.Entity("BillBuddy.API.Models.User", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
