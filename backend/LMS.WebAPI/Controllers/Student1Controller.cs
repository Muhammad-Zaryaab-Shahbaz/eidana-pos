//using LMS.Core.Entities;
//using LMS.Core.Enums;
//using LMS.Service;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace LMS.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Student1Controller : ControllerBase
//    {
//        // GET: api/<CityController>
//        #region Class Variables

//        private Student1Service _stdSVC;

//        #endregion
//        #region Constructors
//        public Student1Controller()
//        {
//            _stdSVC = new Student1Service();
//        }

//        #endregion

//        #region Http Verbs

//        [HttpPost("{Search}")]
//        public IActionResult SearchStudent(STD_StudentDE Student)
//        {
//            List<STD_StudentDE> list = _stdSVC.SearchStudent(Student);
//            return Ok(list);
//        }
//        [HttpGet]

//        public IActionResult GetStudent()
//        {
//            STD_StudentDE stdSC = new STD_StudentDE();
//            List<STD_StudentDE> student = _stdSVC.SearchStudent(stdSC);
//            return Ok(student);
//        }

//        //// GET api/<VocabularyController>/5
//        //[HttpGet("{id}")]
//        //public IActionResult GetVocabularyById(int id)
//        //{
//        //    return "value";
//        //}










//        // POST api/<StudentController>
//        [HttpPost]
//        public IActionResult PostStudent(STD_StudentDE Student)
//        {
//            Student.DBoperation = LMS.Core.Enums.DBoperations.Insert;
//            bool std = _stdSVC.ManageStudent(Student);
//            return Ok(std);
//        }


//        // PUT api/<StudentController>/5
//        [HttpPut]
//        public IActionResult PutStudent(STD_StudentDE Student)
//        {
//            Student.DBoperation = DBoperations.Update;
//            _stdSVC.ManageStudent(Student);
//            return Ok();
//        }

//        [HttpDelete("{id}")]
//        public IActionResult DeleteStudent(int id)
//        {
//            STD_StudentDE Student = new STD_StudentDE();
//            Student.DBoperation = DBoperations.Delete;
//            Student.Id = id;
//            _stdSVC.ManageStudent(Student);
//            return Ok();
//        }
//    }
//    #endregion
//}