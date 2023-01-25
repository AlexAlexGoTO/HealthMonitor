namespace MedixineMonitor.Patients.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
    }
}
