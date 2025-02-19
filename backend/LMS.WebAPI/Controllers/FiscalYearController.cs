using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiscalYearController : ControllerBase
    {
        private FiscalYearService _fySvc;
        public FiscalYearController()
        {
            _fySvc = new FiscalYearService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetFiscalYear()
        {
            List<FiscalYearDE> list = new List<FiscalYearDE>();
            list = _fySvc.SearchFiscalYear(new FiscalYearDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SaveFiscalYear(FiscalYearDE FiscalYear)
        {
            List<FiscalYearDE> list = _fySvc.SearchFiscalYear(FiscalYear);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetFiscalYearById(int id)
        {
            List<FiscalYearDE> list = new List<FiscalYearDE>();
            list = _fySvc.SearchFiscalYear(new FiscalYearDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostFiscalYear(FiscalYearDE FiscalYear)
        {
            FiscalYear.DBoperation = DBoperations.Insert;
            _fySvc.ManageFiscalYear(FiscalYear);
            return Ok();
        }

        [HttpPut]
        public IActionResult PutFiscalYear(FiscalYearDE FiscalYear)
        {
            FiscalYear.DBoperation = DBoperations.Update;
            _fySvc.ManageFiscalYear(FiscalYear);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFiscalYear(int id)
        {
            FiscalYearDE FiscalYearDE = new FiscalYearDE();
            FiscalYearDE.DBoperation = DBoperations.Delete;
            FiscalYearDE.Id = id;
            _fySvc.ManageFiscalYear(FiscalYearDE);
            return Ok();
        }
    }
}
