﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iOSClub.Table.Data;

#nullable disable

namespace iOSClub.Table.Migrations
{
    [DbContext(typeof(SignContext))]
    [Migration("20231027082020_AddMax")]
    partial class AddMax
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("iOSClub.Table.Data.PermissionsModel", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("iOSClub.Table.Data.SignModel", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Academy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PoliticalLandscape")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.ToTable("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
