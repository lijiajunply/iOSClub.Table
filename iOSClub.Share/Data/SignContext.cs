using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace iOSClub.Share.Data;

public sealed class SignContext : DbContext
{
    public SignContext(DbContextOptions<SignContext> options)
        : base(options)
    {
        Database.EnsureCreated();
        //Database.Migrate();
        Students = Set<SignModel>();
        Staffs = Set<PermissionsModel>();
        Events = Set<EventModel>();
    }

    public DbSet<SignModel> Students { get; init; }
    public DbSet<PermissionsModel> Staffs { get; init; }
    public DbSet<EventModel> Events { get; init; }
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