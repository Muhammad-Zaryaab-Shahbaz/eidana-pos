using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private AppointmentService _apptSvc;
        public AppointmentController()
        {
            _apptSvc = new AppointmentService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetAppointment()
        {
            List<AppointmentDE> list = new List<AppointmentDE>();
            list = _apptSvc.SearchAppointment(new AppointmentDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SaveAppointment(AppointmentDE Appointment)
        {
            List<AppointmentDE> list = _apptSvc.SearchAppointment(Appointment);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetAppointmentyId(int id)
        {
            List<AppointmentDE> list = new List<AppointmentDE>();
            list = _apptSvc.SearchAppointment(new AppointmentDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostAppointment(AppointmentDE Appointment)
        {
            Appointment.DBoperation = DBoperations.Insert;
            _apptSvc.ManageAppointment(Appointment);
            return Ok();
        }

        [HttpPut]
        public IActionResult PutAppointment(AppointmentDE Appointment)
        {
            Appointment.DBoperation = DBoperations.Update;
            _apptSvc.ManageAppointment(Appointment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            AppointmentDE AppointmentDE = new AppointmentDE();
            AppointmentDE.DBoperation = DBoperations.Delete;
            AppointmentDE.Id = id;
            _apptSvc.ManageAppointment(AppointmentDE);
            return Ok();
        }
    }
}
