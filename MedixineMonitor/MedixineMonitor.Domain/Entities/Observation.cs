using MedixineMonitor.Domain.Enums;

namespace MedixineMonitor.Domain.Entities;

public class Observation
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public HealthMetrics Type { get; set; }
    public double Value { get; set; }
    public int PatientId { get; set; }
    public string? Description { get; set; }
}
