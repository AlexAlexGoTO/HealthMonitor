using MedixineMonitor.Patients.Entities;
using MedixineMonitor.Patients.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedixineMonitor.Patients.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        public readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IEnumerable<Patient>> Get()
        {
            return await _patientService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Patient> Get(int id)
        {
            return await _patientService.Get(id);
        }

        [HttpPut]
        public async Task<int> Put(Patient patient)
        {
            return await _patientService.UpdateOrCreate(patient);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _patientService.Delete(id);
        }
    }
}