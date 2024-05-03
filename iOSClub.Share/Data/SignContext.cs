﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace iOSClub.Share.Data;

public sealed class SignContext : DbContext
{
    public SignContext(DbContextOptions<SignContext> options)
        : base(options)
    {
        try
        {
            Database.Migrate();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Students = Set<SignModel>();
            Staffs = Set<StaffModel>();
            Events = Set<EventModel>();
            Tasks = Set<TaskModel>();
            Projects = Set<ProjectModel>();
            Resources = Set<ResourceModel>();
        }
    }

    public DbSet<SignModel> Students { get; init; }
    public DbSet<StaffModel> Staffs { get; init; }
    public DbSet<EventModel> Events { get; init; }
    public DbSet<TaskModel> Tasks { get; init; }
    public DbSet<ProjectModel> Projects { get; init; }
    public DbSet<ResourceModel> Resources { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StaffModel>().HasMany(x => x.Tasks).WithMany(x => x.Users);
        modelBuilder.Entity<StaffModel>().HasMany(x => x.Projects).WithMany(x => x.Staffs);
    }
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SignContext>
{
    public SignContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SignContext>();
        optionsBuilder.UseSqlite("Data Source=Data.db");
        return new SignContext(optionsBuilder.Options);
    }
}