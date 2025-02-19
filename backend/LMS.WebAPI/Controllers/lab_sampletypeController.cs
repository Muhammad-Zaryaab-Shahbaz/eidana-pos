//using LMS.Core.Entities;
//using LMS.Core.Enums;
//using LMS.Service;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace LMS.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class lab_sampletypeController : ControllerBase
//    {
//        private lab_sampletypeService _slabSvc;
//        public lab_sampletypeController()
//        {
//            _slabSvc = new lab_sampletypeService();
//        }
//        // HTTP Methods 
//        [HttpGet]
//        public IActionResult GetPage()
//        {
//            List<lab_sampletypeDE> list = new List<lab_sampletypeDE>();
//            list = _slabSvc.Searchlab_sampletype(new lab_sampletypeDE());
//            return Ok(list);
//        }

//        [HttpPost("{Search}")]
//        public IActionResult Searchlab_sampletype(lab_sampletypeDE lab_sampletype)
//        {
//            List<lab_sampletypeDE> list = _slabSvc.Searchlab_sampletype(lab_sampletype);
//            return Ok(list);
//        }


//        [HttpGet("{id}")]
//        public IActionResult Getlab_sampletypeById(int id)
//        {
//            List<lab_sampletypeDE> list = new List<lab_sampletypeDE>();
//            list = _slabSvc.Searchlab_sampletype(new lab_sampletypeDE { Id = id });
//            return Ok(list[0]);

//        }

//        [HttpPost]
//        public IActionResult Postlab_sampletype(lab_sampletypeDE lab_sampletype)
//        {
//            lab_sampletype.DBoperation = DBoperations.Insert;
//            _slabSvc.Managelab_sampletype(lab_sampletype);
//            return Ok();
//        }

//        [HttpPut]
//        public IActionResult Putlab_sampletype(lab_sampletypeDE lab_sampletype)
//        {
//            lab_sampletype.DBoperation = DBoperations.Update;
//            _slabSvc.Managelab_sampletype(lab_sampletype);
//            return Ok();
//        }

//        [HttpDelete("{id}")]
//        public IActionResult Deletelab_sampletype(int id)
//        {
//            lab_sampletypeDE lab_sampletypeDe = new lab_sampletypeDE();
//            lab_sampletypeDe.DBoperation = DBoperations.Delete;
//            lab_sampletypeDe.Id = id;
//            _slabSvc.Managelab_sampletype(lab_sampletypeDe);
//            return Ok();
//        }
//    }
//}
