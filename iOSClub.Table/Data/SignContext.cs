﻿using Microsoft.EntityFrameworkCore;

namespace iOSClub.Table.Data;

public class SignContext : DbContext
{
    public SignContext (DbContextOptions<SignContext> options)
        : base(options) { }

    public DbSet<SignModel> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}