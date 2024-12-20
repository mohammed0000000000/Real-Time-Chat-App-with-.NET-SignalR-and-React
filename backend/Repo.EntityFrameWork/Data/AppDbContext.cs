﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.Repo.EntityFrameWork.Data;

public class AppDbContext: IdentityDbContext<User>
{
	public DbSet<User> Users { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<UserGroup> UserGroups { get; set; }
	public DbSet<Message> Messages { get; set; }
	public DbSet<PrivateMessage> PrivateMessages { get; set; }
	public DbSet<Connection> Connections { get; set; }

	public AppDbContext():base(){ }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer();
    }
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}