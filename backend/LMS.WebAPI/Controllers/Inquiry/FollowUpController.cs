
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS.Core.Entities.Inquiry;
using LMS.Service.Inquiry;
using LMS.Core.Entities.Receivable;
using LMS.Service.Receivable;


namespace LMS.WebAPI.Inquiry.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FollowUpController : ControllerBase
    {
        public readonly IInqService _inqSvc;
        public FollowUpController(IInqService inqSvc)
        {
            _inqSvc = inqSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetFollowUp()
        {
            List<FollowUpDE> list = new List<FollowUpDE>();
            list = _inqSvc.SearchFollowUp(new FollowUpDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchFollowUp(FollowUpDE FollowUp)
        {
            List<FollowUpDE> list = _inqSvc.SearchFollowUp(FollowUp);
            return Ok(list);
        }

        [HttpPost("FollowUp")]
        public IActionResult GetFollowUp(FollowUpDE FollowUp)
        {
            List<FollowUpDE> list = _inqSvc.SearchFollowUp(FollowUp);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetFollowUpById(int id)
        {
            List<FollowUpDE> list = new List<FollowUpDE>();
            list = _inqSvc.SearchFollowUp(new FollowUpDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostFollowUp(FollowUpDE FollowUp)
        {
            FollowUp.DBoperation = DBoperations.Insert;
            _inqSvc.ManageFollowUp(FollowUp);
            return Ok(FollowUp);
        }

        [HttpPut]
        public IActionResult PutFollowUp(FollowUpDE FollowUp)
        {
            FollowUp.DBoperation = DBoperations.Update;
            _inqSvc.ManageFollowUp(FollowUp);
            return Ok(FollowUp);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFollowUp(int id)
        {
            FollowUpDE FollowUpDe = new FollowUpDE();
            FollowUpDe.DBoperation = DBoperations.Delete;
            FollowUpDe.Id = id;
            _inqSvc.ManageFollowUp(FollowUpDe);
            return Ok();
        }

    }
}

