using MedixineMonitor.Patients.Entities;

namespace MedixineMonitor.Patients.Interfaces
{
    public interface IPatientService
    {
        public Task<IEnumerable<Patient>> GetAll();

        public Task<Patient> Get(int id);

        public Task<int> UpdateOrCreate(Patient patient);

        public Task Delete(int id);
    }
}
