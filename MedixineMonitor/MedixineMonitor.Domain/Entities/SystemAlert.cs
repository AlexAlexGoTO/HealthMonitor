namespace MedixineMonitor.Domain.Entities;

public class SystemAlert
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public int PatientId { get; set; }
    public string? Message { get; set; }
}
