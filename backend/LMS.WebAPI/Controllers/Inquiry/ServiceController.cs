
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
    public class ServicesController : ControllerBase
    {
        public readonly IInqService _inqSvc;
        public ServicesController(IInqService inqSvc)
        {
            _inqSvc = inqSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetServices()
        {
            List<ServicesDE> list = new List<ServicesDE>();
            list = _inqSvc.SearchServices(new ServicesDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchServices(ServicesDE Services)
        {
            List<ServicesDE> list = _inqSvc.SearchServices(Services);
            return Ok(list);
        }

        [HttpPost("Services")]
        public IActionResult GetServices(ServicesDE Services)
        {
            List<ServicesDE> list = _inqSvc.SearchServices(Services);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetServicesById(int id)
        {
            List<ServicesDE> list = new List<ServicesDE>();
            list = _inqSvc.SearchServices(new ServicesDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostServices(ServicesDE Services)
        {
            Services.DBoperation = DBoperations.Insert;
            _inqSvc.ManageServices(Services);
            return Ok(Services);
        }

        [HttpPut]
        public IActionResult PutServices(ServicesDE Services)
        {
            Services.DBoperation = DBoperations.Update;
            _inqSvc.ManageServices(Services);
            return Ok(Services);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteServices(int id)
        {
            ServicesDE ServicesDe = new ServicesDE();
            ServicesDe.DBoperation = DBoperations.Delete;
            ServicesDe.Id = id;
            _inqSvc.ManageServices(ServicesDe);
            return Ok();
        }

    }
}

