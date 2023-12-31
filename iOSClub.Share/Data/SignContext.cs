﻿using Microsoft.EntityFrameworkCore;

namespace iOSClub.Share.Data;

public class SignContext : DbContext
{
    public SignContext(DbContextOptions<SignContext> options)
        : base(options) { }

    public DbSet<SignModel> Students { get; set; }
    public DbSet<PermissionsModel> Staffs { get; set; }
}