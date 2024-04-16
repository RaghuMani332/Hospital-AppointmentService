using Dapper;
using Hospital_AppointmentService.DTO;
using Hospital_AppointmentService.Entity;
using Hospital_AppointmentService.Service.Interface;
using Hospital_HospitalService.Context;
using System.Net.Http;
namespace Hospital_AppointmentService.Service.Impl
{
    public class AppointmentServiceImpl(DapperContext context, IHttpClientFactory httpClientFactory) : IAppointmentService
    {
        public int createAppointment(AppointmentRequest request, int DoctorId,int userId)
        {
            AppointmentEntity appointmentEntity = MapToEntity(request, DoctorId, userId);

            // Define the SQL query
            string sql = @"
                INSERT INTO Appointments (PatientName, Age, Issue, BookedBy, BookedWith, DoctorName, Specialization, AppointmentDate, Department, Status)
                VALUES (@PatientName, @Age, @Issue, @BookedBy, @BookedWith, @DoctorName, @Specialization, @AppointmentDate, @Department, @Status)
            ";
           return context.getConnection().Execute(sql, appointmentEntity);
        }
        private AppointmentEntity MapToEntity(AppointmentRequest request, int DoctorId, int userId)
        {
           // UserObject user= getUserById(userId);
            DoctorObject doctor = getDoctorBylId(DoctorId);
            return new AppointmentEntity
            {
                PatientName = request.PatientName,
                Issue=request.Issue,
                BookedBy=userId,
                BookedWith=doctor.dId,
                AppointmentDate=request.Date,
                Age=request.Age,
                DoctorName=doctor.doctorName,
                Specialization=doctor.specialization,
                Department= doctor.specialization,
                Status="pending"
            };
        }
        public UserObject getUserById(int userId)
        {
            var httpclient = httpClientFactory.CreateClient("userById");
            var responce = httpclient.GetAsync($"getbyid?id={userId}").Result;
            if (responce.IsSuccessStatusCode)
            {
                return responce.Content.ReadFromJsonAsync<UserObject>().Result;
            }
            throw new Exception("UserNotFound Create User FIRST OE TRY DIFFERENT EMAIL ID");
        }
        public DoctorObject getDoctorBylId(int DoctorId)
        {
            var httpclient = httpClientFactory.CreateClient("DoctorById");
            var responce = httpclient.GetAsync($"{DoctorId}").Result;
            if (responce.IsSuccessStatusCode)
            {
                return responce.Content.ReadFromJsonAsync<DoctorObject>().Result;
            }
            throw new Exception("UserNotFound Create User FIRST OE TRY DIFFERENT EMAIL ID");
        }

        public List<AppointmentEntity> getAllAppointmentByPatientId(int id)
        {
            String query = "select * from Appointments where BookedBy = @id";
            return context.getConnection().Query<AppointmentEntity>(query,new { id=id }).ToList();
        }

        public List<AppointmentEntity> getAppointByDoctorId(int id)
        {
            String query = "select * from Appointments where BookedWith = @id";
            return context.getConnection().Query<AppointmentEntity>(query, new { id = id }).ToList();
        }

        public AppointmentEntity getappointmentbyid(int id)
        {
            String query = "select * from Appointments where AppointmentId = @id";
            return context.getConnection().Query<AppointmentEntity>(query, new { id = id }).FirstOrDefault();
        }

        public AppointmentEntity UpdateAppointment(AppointmentRequest request, string? patientId, int AppointmentId)
        {
            // Get the existing appointment by its ID
            AppointmentEntity existingAppointment = getappointmentbyid(AppointmentId);

            // If the appointment doesn't exist, return null or throw an exception
            if (existingAppointment == null)
            {
                throw new Exception("Appointment not found");
            }

            // Update the appointment details with the new values from the request
            existingAppointment.PatientName = request.PatientName;
            existingAppointment.Age = request.Age;
            existingAppointment.Issue = request.Issue;
            existingAppointment.BookedBy = int.Parse(patientId); // Assuming you want to update patient ID as well
            existingAppointment.AppointmentDate = request.Date;

            // Update the appointment in the database
            string sql = @"
        UPDATE Appointments
        SET PatientName = @PatientName, 
            Age = @Age, 
            Issue = @Issue, 
            BookedBy = @BookedBy, 
            AppointmentDate = @AppointmentDate,
            Status = @Status
        WHERE AppointmentId = @AppointmentId
    ";

            // Execute the update query
            context.getConnection().Execute(sql, existingAppointment);

            // Return the updated appointment
            return getappointmentbyid(AppointmentId);
        }


        public AppointmentEntity UpdateStatus(int appointmentid, string status)
        {
            String query = "update Appointments set status = @sts where AppointmentId = @id";
             context.getConnection().Query<AppointmentEntity>(query, new { id = appointmentid , sts=status});
            return getappointmentbyid(appointmentid);
        }

       
    }
}
