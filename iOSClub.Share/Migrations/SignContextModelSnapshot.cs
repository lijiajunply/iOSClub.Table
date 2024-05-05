﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using iOSClub.Share;

#nullable disable

namespace iOSClub.Share.Migrations
{
    [DbContext(typeof(SignContext))]
    partial class SignContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("ProjectModelStaffModel", b =>
                {
                    b.Property<int>("ProjectsId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StaffsUserId")
                        .HasColumnType("varchar(10)");

                    b.HasKey("ProjectsId", "StaffsUserId");

                    b.HasIndex("StaffsUserId");

                    b.ToTable("ProjectModelStaffModel");
                });

            modelBuilder.Entity("StaffModelTaskModel", b =>
                {
                    b.Property<int>("TasksId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UsersUserId")
                        .HasColumnType("varchar(10)");

                    b.HasKey("TasksId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("StaffModelTaskModel");
                });

            modelBuilder.Entity("iOSClub.Share.Data.EventModel", b =>
                {
                    b.Property<string>("Link")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Cover")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Digest")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Link");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("iOSClub.Share.Data.ProjectModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(512)");

                    b.Property<string>("EndTime")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("StartTime")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("iOSClub.Share.Data.ResourceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Tag")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("iOSClub.Share.Data.StaffModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("iOSClub.Share.Data.StudentModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Academy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<string>("PoliticalLandscape")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("iOSClub.Share.Data.TaskModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("EndTime")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("iOSClub.Share.Data.ToolModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Tag")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("ProjectModelStaffModel", b =>
                {
                    b.HasOne("iOSClub.Share.Data.ProjectModel", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iOSClub.Share.Data.StaffModel", null)
                        .WithMany()
                        .HasForeignKey("StaffsUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StaffModelTaskModel", b =>
                {
                    b.HasOne("iOSClub.Share.Data.TaskModel", null)
                        .WithMany()
                        .HasForeignKey("TasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iOSClub.Share.Data.StaffModel", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("iOSClub.Share.Data.TaskModel", b =>
                {
                    b.HasOne("iOSClub.Share.Data.ProjectModel", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("iOSClub.Share.Data.ProjectModel", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
