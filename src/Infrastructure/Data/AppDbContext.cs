using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cpu> Cpus => Set<Cpu>();
    public DbSet<Motherboard> Motherboards => Set<Motherboard>();
    public DbSet<Ram> Rams => Set<Ram>();
    public DbSet<Gpu> Gpus => Set<Gpu>();
    public DbSet<Psu> Psus => Set<Psu>();
    public DbSet<PcCase> PcCases => Set<PcCase>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Store List<string> as comma-separated JSON strings in SQLite
        modelBuilder.Entity<Cpu>()
            .Property(c => c.SupportedMemoryTypes)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        modelBuilder.Entity<PcCase>()
            .Property(c => c.SupportedMotherboardFormFactors)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        modelBuilder.Entity<PcCase>()
            .Property(c => c.SupportedPsuFormFactors)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}
