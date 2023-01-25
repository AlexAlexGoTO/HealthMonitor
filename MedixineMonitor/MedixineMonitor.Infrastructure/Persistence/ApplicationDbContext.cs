using MedixineMonitor.Application.Common.Dto;
using MedixineMonitor.Application.Common.Interfaces;
using MedixineMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MedixineMonitor.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Observation> Observations => Set<Observation>();
    public DbSet<SystemAlert> SystemAlerts => Set<SystemAlert>();
    public DbSet<PatientDto> VW_Patients => Set<PatientDto>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<Observation>(entity => {
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(t => t.Value).IsRequired();
            entity.Property(t => t.Type).IsRequired();
            entity.Property(t => t.Name).IsRequired();
        });

        builder.Entity<SystemAlert>(entity => {
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(t => t.ItemId).IsRequired();
            entity.Property(t => t.Message).IsRequired();
        });

        builder.Entity<PatientDto>(e =>
        {
            e.HasNoKey();
            e.ToView("Patients");
        });

        base.OnModelCreating(builder);
    }
}