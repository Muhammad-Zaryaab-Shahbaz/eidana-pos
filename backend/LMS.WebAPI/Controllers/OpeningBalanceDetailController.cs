using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpeningBalanceDetailController : ControllerBase
    {
        private OpeningBalanceDetailService _openingSvc;
        public OpeningBalanceDetailController()
        {
            _openingSvc = new OpeningBalanceDetailService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult Getacc_openingbalancedetail()
        {
            List<OpeningBalanceDetailDE> list = new List<OpeningBalanceDetailDE>();
            list = _openingSvc.SearchOpeningBalanceDetail(new OpeningBalanceDetailDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchOpeningBalanceDetail(OpeningBalanceDetailDE openingBalance)
        {
            List<OpeningBalanceDetailDE> list = _openingSvc.SearchOpeningBalanceDetail(openingBalance);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetOpeningBalanceDetailById(int id)
        {
            List<OpeningBalanceDetailDE> list = new List<OpeningBalanceDetailDE>();
            list = _openingSvc.SearchOpeningBalanceDetail(new OpeningBalanceDetailDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostOpeningBalanceDetail(OpeningBalanceDetailDE openingBalance)
        {
            openingBalance.DBoperation = DBoperations.Insert;
            _openingSvc.ManageOpeningBalanceDetail(openingBalance);
            return Ok();
        }

        [HttpPut]
        public IActionResult PutOpeningBalanceDetail(OpeningBalanceDetailDE openingBalance)
        {
            openingBalance.DBoperation = DBoperations.Update;
            _openingSvc.ManageOpeningBalanceDetail(openingBalance);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOpeningBalanceDetail(int id)
        {
            OpeningBalanceDetailDE OpeningBalanceDetailDe = new OpeningBalanceDetailDE();
            OpeningBalanceDetailDe.DBoperation = DBoperations.Delete;
            OpeningBalanceDetailDe.Id = id;
            _openingSvc.ManageOpeningBalanceDetail(OpeningBalanceDetailDe);
            return Ok();
        }
    }
}
