
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
    public class ServiceChargesController : ControllerBase
    {
        public readonly IInqService _inqSvc;
        public ServiceChargesController(IInqService inqSvc)
        {
            _inqSvc = inqSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetServiceCharges()
        {
            List<ServiceChargesDE> list = new List<ServiceChargesDE>();
            list = _inqSvc.SearchServiceCharges(new ServiceChargesDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchServiceCharges(ServiceChargesDE ServiceCharges)
        {
            List<ServiceChargesDE> list = _inqSvc.SearchServiceCharges(ServiceCharges);
            return Ok(list);
        }

        [HttpPost("ServiceCharges")]
        public IActionResult GetServiceCharges(ServiceChargesDE ServiceCharges)
        {
            List<ServiceChargesDE> list = _inqSvc.SearchServiceCharges(ServiceCharges);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetServiceChargesById(int id)
        {
            List<ServiceChargesDE> list = new List<ServiceChargesDE>();
            list = _inqSvc.SearchServiceCharges(new ServiceChargesDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostServiceCharges(ServiceChargesDE ServiceCharges)
        {
            ServiceCharges.DBoperation = DBoperations.Insert;
            _inqSvc.ManageServiceCharges(ServiceCharges);
            return Ok(ServiceCharges);
        }

        [HttpPut]
        public IActionResult PutServiceCharges(ServiceChargesDE ServiceCharges)
        {
            ServiceCharges.DBoperation = DBoperations.Update;
            _inqSvc.ManageServiceCharges(ServiceCharges);
            return Ok(ServiceCharges);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteServiceCharges(int id)
        {
            ServiceChargesDE ServiceChargesDe = new ServiceChargesDE();
            ServiceChargesDe.DBoperation = DBoperations.Delete;
            ServiceChargesDe.Id = id;
            _inqSvc.ManageServiceCharges(ServiceChargesDe);
            return Ok();
        }

    }
}

