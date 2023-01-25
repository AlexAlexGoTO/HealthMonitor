using MedixineMonitor.Application.Common.Dto;
using MedixineMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedixineMonitor.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Observation> Observations { get; }
    DbSet<SystemAlert> SystemAlerts { get; }
    DbSet<PatientDto> VW_Patients { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

