//using LMS.Core.Entities;
//using LMS.Core.Enums;
//using LMS.Service;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace LMS.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PatientLabTestController : ControllerBase
//    {
//        private PatientLabTestService _plabSvc;
//        public PatientLabTestController()
//        {
//            _plabSvc = new PatientLabTestService();
//        }
//        // HTTP Methods 
//        [HttpGet]
//        public IActionResult Getlab_patientlabtest()
//        {
//            List<PatientLabTestDE> list = new List<PatientLabTestDE>();
//            list = _plabSvc.SearchPatientLabTest(new PatientLabTestDE());
//            return Ok(list);
//        }

//        [HttpPost("{Search}")]
//        public IActionResult SearchPatientLabTest(PatientLabTestDE PatientLabTest)
//        {
//            List<PatientLabTestDE> list = _plabSvc.SearchPatientLabTest(PatientLabTest);
//            return Ok(list);
//        }


//        [HttpGet("{id}")]
//        public IActionResult GetPatientLabTestById(int id)
//        {
//            List<PatientLabTestDE> list = new List<PatientLabTestDE>();
//            list = _plabSvc.SearchPatientLabTest(new PatientLabTestDE { Id = id });
//            return Ok(list[0]);

//        }

//        [HttpPost]
//        public IActionResult PostPatientLabTest(PatientLabTestDE PatientLabTest)
//        {
//            PatientLabTest.DBoperation = DBoperations.Insert;
//            _plabSvc.ManagePatientLabTest(PatientLabTest);
//            return Ok();
//        }

//        [HttpPut]
//        public IActionResult PutPatientLabTest(PatientLabTestDE PatientLabTest)
//        {
//            PatientLabTest.DBoperation = DBoperations.Update;
//            _plabSvc.ManagePatientLabTest(PatientLabTest);
//            return Ok();
//        }

//        [HttpDelete("{id}")]
//        public IActionResult DeletePatientLabTest(int id)
//        {
//            PatientLabTestDE PatientLabTestDe = new PatientLabTestDE();
//            PatientLabTestDe.DBoperation = DBoperations.Delete;
//            PatientLabTestDe.Id = id;
//            _plabSvc.ManagePatientLabTest(PatientLabTestDe);
//            return Ok();
//        }
//    }
//}
