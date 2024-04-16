using Hospital_AppointmentService.DTO;
using Hospital_AppointmentService.Entity;

namespace Hospital_AppointmentService.Service.Interface
{
    public interface IAppointmentService
    {
        int createAppointment(AppointmentRequest request, int doctorId,int userId);
        List<AppointmentEntity> getAllAppointmentByPatientId(int id);
        List<AppointmentEntity> getAppointByDoctorId(int id);
        AppointmentEntity getappointmentbyid(int id);
        AppointmentEntity UpdateAppointment(AppointmentRequest request, string? patientId, int AppointmentId);
        AppointmentEntity UpdateStatus(int appointmentid, string status);
    }
}
