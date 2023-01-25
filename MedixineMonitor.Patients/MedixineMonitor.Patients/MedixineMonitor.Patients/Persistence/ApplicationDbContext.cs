using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MedixineMonitor.Patients.Entities;
using MedixineMonitor.Patients.Interfaces;

namespace MedixineMonitor.Patients.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<Patient>(entity => {
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(t => t.Age).IsRequired();
            entity.Property(t => t.Name).IsRequired();
            entity.Property(t => t.Address).IsRequired();
            entity.ToTable("Patient", "patients");
        });

        base.OnModelCreating(builder);
    }
}
