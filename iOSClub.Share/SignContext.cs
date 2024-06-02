using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using iOSClub.Share.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace iOSClub.Share;

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
            Students = Set<StudentModel>();
            Staffs = Set<StaffModel>();
            Events = Set<EventModel>();
            Tasks = Set<TaskModel>();
            Projects = Set<ProjectModel>();
            Resources = Set<ResourceModel>();
            Tools = Set<ToolModel>();
        }
    }

    public DbSet<StudentModel> Students { get; init; }
    public DbSet<StaffModel> Staffs { get; init; }
    public DbSet<EventModel> Events { get; init; }
    public DbSet<TaskModel> Tasks { get; init; }
    public DbSet<ProjectModel> Projects { get; init; }
    public DbSet<ResourceModel> Resources { get; init; }
    public DbSet<ToolModel> Tools { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("TaskModelId")
            .StartsAt(1)
            .IncrementsBy(1);
        modelBuilder.Entity<TaskModel>()
            .Property(o => o.Id)
            .HasDefaultValueSql("nextval('\"TaskModelId\"')");

        modelBuilder.HasSequence<int>("ProjectModelId")
            .StartsAt(1)
            .IncrementsBy(1);
        modelBuilder.Entity<ProjectModel>()
            .Property(o => o.Id)
            .HasDefaultValueSql("nextval('\"ProjectModelId\"')");

        modelBuilder.HasSequence<int>("ResourceModelId")
            .StartsAt(1)
            .IncrementsBy(1);
        modelBuilder.Entity<ResourceModel>()
            .Property(o => o.Id)
            .HasDefaultValueSql("nextval('\"ResourceModelId\"')");

        modelBuilder.HasSequence<int>("ToolModelId")
            .StartsAt(1)
            .IncrementsBy(1);
        modelBuilder.Entity<ToolModel>()
            .Property(o => o.Id)
            .HasDefaultValueSql("nextval('\"ToolModelId\"')");

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

public static class DataTool
{
    public static string StringToHash(string s)
    {
        var data = Encoding.UTF8.GetBytes(s);
        var hash = MD5.HashData(data);
        var hashStringBuilder = new StringBuilder();
        foreach (var t in hash)
            hashStringBuilder.Append(t.ToString("x2"));
        return hashStringBuilder.ToString();
    }

    public static string GetProperties<T>(T t)
    {
        StringBuilder builder = new StringBuilder();
        if (t == null) return builder.ToString();

        var properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

        if (properties.Length <= 0) return builder.ToString();

        foreach (var item in properties)
        {
            var name = item.Name;
            var value = item.GetValue(t, null);
            if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
            {
                builder.Append($"{name}:{value ?? "null"},");
            }
        }

        return builder.ToString();
    }
}

public abstract class DataModel
{
    public override string ToString() => $"{GetType()} : {DataTool.GetProperties(this)}";
}