
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS.Core.Entities.Inquiry;
using LMS.Service.Inquiry;



namespace LMS.WebAPI.Inquiry.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        public readonly IInqService _inqSvc;
        public InquiryController(IInqService inqSvc)
        {
            _inqSvc = inqSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetInquiry()
        {
            List<InquiryDE> list = new List<InquiryDE>();
            list = _inqSvc.SearchInquiry(new InquiryDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchInquiry(InquiryDE Inquiry)
        {
            List<InquiryDE> list = _inqSvc.SearchInquiry(Inquiry);
            return Ok(list);
        }

        [HttpPost("Inquiry")]
        public IActionResult GetInquiry(InquiryDE Inquiry)
        {
            List<InquiryDE> list = _inqSvc.SearchInquiry(Inquiry);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetInquiryById(int id)
        {
            List<InquiryDE> list = new List<InquiryDE>();
            list = _inqSvc.SearchInquiry(new InquiryDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostInquiry(InquiryDE Inquiry)
        {
            Inquiry.DBoperation = DBoperations.Insert;
            _inqSvc.ManageInquiry(Inquiry);
            return Ok(Inquiry);
        }

        [HttpPut]
        public IActionResult PutInquiry(InquiryDE Inquiry)
        {
            Inquiry.DBoperation = DBoperations.Update;
            _inqSvc.ManageInquiry(Inquiry);
            return Ok(Inquiry);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInquiry(int id)
        {
            InquiryDE InquiryDe = new InquiryDE();
            InquiryDe.DBoperation = DBoperations.Delete;
            InquiryDe.Id = id;
            _inqSvc.ManageInquiry(InquiryDe);
            return Ok();
        }

    }
}

