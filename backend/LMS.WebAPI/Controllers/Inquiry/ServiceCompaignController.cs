
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
    public class ServiceCompaignController : ControllerBase
    {
        public readonly IInqService _inqSvc;
        public ServiceCompaignController(IInqService inqSvc)
        {
            _inqSvc = inqSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetServiceCompaign()
        {
            List<ServiceCompaignDE> list = new List<ServiceCompaignDE>();
            list = _inqSvc.SearchServiceCompaign(new ServiceCompaignDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchServiceCompaign(ServiceCompaignDE Service)
        {
            List<ServiceCompaignDE> list = _inqSvc.SearchServiceCompaign(Service);
            return Ok(list);
        }

        [HttpPost("ServiceCompaign")]
        public IActionResult GetServiceCompaign(ServiceCompaignDE Service)
        {
            List<ServiceCompaignDE> list = _inqSvc.SearchServiceCompaign(Service);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetServiceCompaignById(int id)
        {
            List<ServiceCompaignDE> list = new List<ServiceCompaignDE>();
            list = _inqSvc.SearchServiceCompaign(new ServiceCompaignDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostServiceCompaign(ServiceCompaignDE Service)
        {
            Service.DBoperation = DBoperations.Insert;
            _inqSvc.ManageServiceCompaign(Service);
            return Ok(Service);
        }

        [HttpPut]
        public IActionResult PutServiceCompaign(ServiceCompaignDE Service)
        {
            Service.DBoperation = DBoperations.Update;
            _inqSvc.ManageServiceCompaign(Service);
            return Ok(Service);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteServiceCompaign(int id)
        {
            ServiceCompaignDE ServiceCompaignDe = new ServiceCompaignDE();
            ServiceCompaignDe.DBoperation = DBoperations.Delete;
            ServiceCompaignDe.Id = id;
            _inqSvc.ManageServiceCompaign(ServiceCompaignDe);
            return Ok();
        }

    }
}

