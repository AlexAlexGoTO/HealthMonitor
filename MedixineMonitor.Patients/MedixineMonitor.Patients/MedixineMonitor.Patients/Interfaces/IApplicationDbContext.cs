using MedixineMonitor.Patients.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedixineMonitor.Patients.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Patient> Patients { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}


