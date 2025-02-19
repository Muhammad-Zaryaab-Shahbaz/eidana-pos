using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleTypeController : ControllerBase
    {
        private SampleTypeService _slabSvc;
        public SampleTypeController()
        {
            _slabSvc = new SampleTypeService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetPage()
        {
            List<SampleTypeDE> list = new List<SampleTypeDE>();
            list = _slabSvc.SearchSampleType(new SampleTypeDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchSampleType(SampleTypeDE sampleType)
        {
            List<SampleTypeDE> list = _slabSvc.SearchSampleType(sampleType);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetSampleTypeById(int id)
        {
            List<SampleTypeDE> list = new List<SampleTypeDE>();
            list = _slabSvc.SearchSampleType(new SampleTypeDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostSampleType(SampleTypeDE sampleType)
        {
            sampleType.DBoperation = DBoperations.Insert;
            _slabSvc.ManageSampleType(sampleType);
            return Ok();
        }

        [HttpPut]
        public IActionResult PutSampleType(SampleTypeDE sampleType)
        {
            sampleType.DBoperation = DBoperations.Update;
            _slabSvc.ManageSampleType(sampleType);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletelab_sampletype(int id)
        {
            SampleTypeDE SampleTypeDe = new SampleTypeDE();
            SampleTypeDe.DBoperation = DBoperations.Delete;
            SampleTypeDe.Id = id;
            _slabSvc.ManageSampleType(SampleTypeDe);
            return Ok();
        }
    }
}
