using Microsoft.EntityFrameworkCore;

namespace iOSClub.Table.Data;

public class SignContext : DbContext
{
    public SignContext(DbContextOptions<SignContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<SignModel> Students { get; set; }
    public DbSet<PermissionsModel> Staffs { get; set; }
}