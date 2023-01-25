using MedixineMonitor.Patients.Entities;
using MedixineMonitor.Patients.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedixineMonitor.Patients.Services;

public class PatientService : IPatientService
{
    private readonly IApplicationDbContext _context;

    public PatientService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAll()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient> Get(int id)
    {
        return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> UpdateOrCreate(Patient patient)
    {
        _context.Patients.Add(patient);

        await _context.SaveChangesAsync(new CancellationToken());

        return patient.Id;
    }

    public async Task Delete(int id)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(sa => sa.Id == id);

        if (patient != null)
        {
            patient.IsDeleted = true;

            _context.Patients.Update(patient);

            await _context.SaveChangesAsync(new CancellationToken());
        }
    }
}
