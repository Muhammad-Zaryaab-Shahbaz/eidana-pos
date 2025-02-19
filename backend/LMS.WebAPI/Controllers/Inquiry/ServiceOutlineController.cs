using LMS.Core.Entities;
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
    public class ServiceOutlineController : ControllerBase
    {
        public readonly IInqService _inqSvc;
        public ServiceOutlineController(IInqService inqSvc)
        {
            _inqSvc = inqSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetServiceOutline()
        {
            List<ServiceOutlineDE> list = new List<ServiceOutlineDE>();
            list = _inqSvc.SearchServiceOutline(new ServiceOutlineDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchServiceOutline(ServiceOutlineDE ServiceOutline)
        {
            List<ServiceOutlineDE> list = _inqSvc.SearchServiceOutline(ServiceOutline);
            return Ok(list);
        }

        [HttpPost("ServiceOutline")]
        public IActionResult GetServiceOutline(ServiceOutlineDE ServiceOutline)
        {
            List<ServiceOutlineDE> list = _inqSvc.SearchServiceOutline(ServiceOutline);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetServiceOutlineById(int id)
        {
            List<ServiceOutlineDE> list = new List<ServiceOutlineDE>();
            list = _inqSvc.SearchServiceOutline(new ServiceOutlineDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostServiceOutline(ServiceOutlineDE ServiceOutline)
        {
            ServiceOutline.DBoperation = DBoperations.Insert;
            _inqSvc.ManageServiceOutline(ServiceOutline);
            return Ok(ServiceOutline);
        }

        [HttpPut]
        public IActionResult PutServiceOutline(ServiceOutlineDE ServiceOutline)
        {
            ServiceOutline.DBoperation = DBoperations.Update;
            _inqSvc.ManageServiceOutline(ServiceOutline);
            return Ok(ServiceOutline);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteServiceOutline(int id)
        {
            ServiceOutlineDE ServiceOutlineDe = new ServiceOutlineDE();
            ServiceOutlineDe.DBoperation = DBoperations.Delete;
            ServiceOutlineDe.Id = id;
            _inqSvc.ManageServiceOutline(ServiceOutlineDe);
            return Ok();
        }

    }
}

