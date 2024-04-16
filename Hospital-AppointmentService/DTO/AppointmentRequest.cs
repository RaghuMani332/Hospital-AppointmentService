namespace Hospital_AppointmentService.DTO
{
    public class AppointmentRequest
    {
        public string PatientName { get; set; }
        public string Issue { get; set; }
        public int PatentId { get; set; }
        public DateTime Date { get; set; }
        public int Age { get; set; }

    }
}
