using DeweyDecimalClassification.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeweyDecimalClassification.EfCore.Context;

public class DeweyDecimalClassificationDbContext : DbContext
{
    public DbSet<DeweyEntry> DeweyEntries { get; set; }
    
    public DeweyDecimalClassificationDbContext() { }

    public DeweyDecimalClassificationDbContext(DbContextOptions<DeweyDecimalClassificationDbContext> options)
        : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=DeweyDecimalClassification.db");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeweyEntry>()
            .HasOne(d => d.Parent)
            .WithMany(p => p.Children)
            .HasForeignKey(d => d.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}