using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentFormController : ControllerBase
    {
        private StudentFormService _stdSvc;
        public StudentFormController()
        {
            _stdSvc = new StudentFormService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetStudentForm()
        {
            List<StudentFormDE> list = new List<StudentFormDE>();
            list = _stdSvc.SearchStudentForm(new StudentFormDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchStudentForm(StudentFormDE StudentForm)
        {
            List<StudentFormDE> list = _stdSvc.SearchStudentForm(StudentForm);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetStudentFormById(int id)
        {
            List<StudentFormDE> list = new List<StudentFormDE>();
            list = _stdSvc.SearchStudentForm(new StudentFormDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostStudentForm(StudentFormDE StudentForm)
        {
            StudentForm.DBoperation = DBoperations.Insert;
            _stdSvc.ManageStudentForm(StudentForm);
            return Ok();
        }

        [HttpPut]
        public IActionResult PutStudentForm(StudentFormDE StudentForm)
        {
            StudentForm.DBoperation = DBoperations.Update;
            _stdSvc.ManageStudentForm(StudentForm);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudentForm(int id)
        {
            StudentFormDE StudentFormDe = new StudentFormDE();
            StudentFormDe.DBoperation = DBoperations.Delete;
            StudentFormDe.Id = id;
            _stdSvc.ManageStudentForm(StudentFormDe);
            return Ok();
        }
    }
}
