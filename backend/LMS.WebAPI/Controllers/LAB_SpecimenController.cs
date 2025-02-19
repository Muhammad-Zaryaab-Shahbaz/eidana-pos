using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.DAL;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LAB_SpecimenController : ControllerBase
    {
        private LAB_SpecimenService _lABsSvc;
        public LAB_SpecimenController()
        {
            _lABsSvc = new LAB_SpecimenService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetLAB_Specimen()
        {
            List<LAB_SpecimenDE> list = new List<LAB_SpecimenDE>();
            list = _lABsSvc.SearchLAB_Specimen(new LAB_SpecimenDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchLAB_Specimen(LAB_SpecimenDE LAB_Specimen)
        {
            List<LAB_SpecimenDE> list = _lABsSvc.SearchLAB_Specimen(LAB_Specimen);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetLAB_SpecimenById(int id)
        {
            List<LAB_SpecimenDE> list = new List<LAB_SpecimenDE>();
            list = _lABsSvc.SearchLAB_Specimen(new LAB_SpecimenDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostLAB_Specimen(LAB_SpecimenDE LAB_Specimen)
        {
            LAB_Specimen.DBoperation = DBoperations.Insert;
            _lABsSvc.ManageLAB_Specimen(LAB_Specimen);
            return Ok();
        }

        [HttpPut]
        public IActionResult PutLAB_Specimen(LAB_SpecimenDE LAB_Specimen)
        {
            LAB_Specimen.DBoperation = DBoperations.Update;
            _lABsSvc.ManageLAB_Specimen(LAB_Specimen);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLAB_Specimen(int id)
        {
            LAB_SpecimenDE LAB_SpecimenDe = new LAB_SpecimenDE();
            LAB_SpecimenDe.DBoperation = DBoperations.Delete;
            LAB_SpecimenDe.Id = id;
            _lABsSvc.ManageLAB_Specimen(LAB_SpecimenDe);
            return Ok();
        }
    }
}
