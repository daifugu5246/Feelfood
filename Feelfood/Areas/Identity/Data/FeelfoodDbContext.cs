using Feelfood.Areas.Identity.Data;
using Feelfood.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Feelfood.Data;

public class FeelfoodDbContext : IdentityDbContext<FeelfoodUser>
{
    public FeelfoodDbContext(DbContextOptions<FeelfoodDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<FeelfoodUser>()
            .HasMany(e => e.Orders)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id);

        builder.Entity<FeelfoodUser>()
            .HasOne(e => e.Job)
            .WithOne(e => e.User)
            .HasForeignKey<FeelfoodUser>(e => e.JobId);

        builder.Entity<JobModel>()
            .HasOne(e => e.Order)
            .WithOne(e => e.Job)
            .HasForeignKey<JobModel>(e => e.OrderId)
            .HasPrincipalKey<OrderModel>(e => e.Id);

        builder.Entity<OrderModel>()
            .HasOne(e => e.Job)
            .WithOne(e => e.Order)
            .HasForeignKey<OrderModel>(e => e.JobId)
            .HasPrincipalKey<JobModel>(e => e.Id);




        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<JobModel> Jobs { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
}
