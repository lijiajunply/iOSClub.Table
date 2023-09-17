using Microsoft.EntityFrameworkCore;

namespace TableData;

public class SignContext : DbContext
{
    public SignContext (DbContextOptions<SignContext> options)
        : base(options) { }

    public DbSet<SignModel> Students { get; set; }
}