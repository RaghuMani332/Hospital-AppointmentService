using Hospital_AppointmentService.DTO;
using Hospital_AppointmentService.Entity;
using Hospital_AppointmentService.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace Hospital_AppointmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController(IAppointmentService service) : ControllerBase
    {

        [HttpPost("DoctorId/{DoctorId:int}")]
        [Authorize(Roles = "PATIENT")]
        public int CreateAppointment(AppointmentRequest request,int DoctorId)
        {
            var userId = User.FindFirstValue("UserId");
            
           return service.createAppointment(request, DoctorId,int.Parse(userId));
        }
        [HttpGet("getappointmentbyid")]
       public AppointmentEntity getappointmentbyid(int Id)
        {
            return service.getappointmentbyid(Id);
        }

        [HttpGet("getAllAppointmentByPatient")]
        [Authorize]
        public List<AppointmentEntity> getAllAppointmentByPatientId()
        {
            var PatientId = User.FindFirstValue("UserId");
            return service.getAllAppointmentByPatientId(int.Parse(PatientId));
        }

        [HttpGet("getAppointByDoctorId")]
        public List<AppointmentEntity> getAppointByDoctorId(int id)
        {
            return service.getAppointByDoctorId(id);
        }
        [HttpPut("UpdateAppointment")]
        [Authorize(Roles ="PATIENT")]
        public AppointmentEntity UpdateAppointment(AppointmentRequest request,int AppointmentId)//-->by patient
        {
            var PatientId = User.FindFirstValue("UserId");
            return service.UpdateAppointment(request, PatientId, AppointmentId);
        }
        [HttpPost("UpdateStatus")]
        [Authorize(Roles ="DOCTOR")]
        public AppointmentEntity UpdateStatus(int Appointmentid,String Status)//-->by Doctor
        {
           return service.UpdateStatus(Appointmentid, Status);
        }
    }
}
