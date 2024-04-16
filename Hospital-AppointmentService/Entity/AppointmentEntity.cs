namespace Hospital_AppointmentService.Entity
{
    public class AppointmentEntity
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Issue { get; set; }
        public int BookedBy { get; set; }//patient id
        public int BookedWith { get; set; }// doctor id
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }

    }
}
