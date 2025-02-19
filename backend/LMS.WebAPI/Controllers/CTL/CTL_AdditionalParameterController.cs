using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS.Core.Entities.CTL;
using LMS.Service.CTL;


namespace LMS.WebAPI.Controllers.CTL
{

    [Route("api/[controller]")]
    [ApiController]
    public class CTL_AdditionalParameterController : ControllerBase
    {
        public readonly ICtlService _ctlSvc;
        public CTL_AdditionalParameterController(ICtlService ctlSvc)
        {
            _ctlSvc = ctlSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetCTL_AdditionalParameter()
        {
            List<CTL_AdditionalParameterDE> list = new List<CTL_AdditionalParameterDE>();
            list = _ctlSvc.SearchCTL_AdditionalParameter(new CTL_AdditionalParameterDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchCTL_AdditionalParameter(CTL_AdditionalParameterDE AdditionalParameter)
        {
            List<CTL_AdditionalParameterDE> list = _ctlSvc.SearchCTL_AdditionalParameter(AdditionalParameter);
            return Ok(list);
        }

        [HttpPost("CTL_AdditionalParameter")]
        public IActionResult GetCTL_AdditionalParameter(CTL_AdditionalParameterDE AdditionalParameter)
        {
            List<CTL_AdditionalParameterDE> list = _ctlSvc.SearchCTL_AdditionalParameter(AdditionalParameter);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetCTL_AdditionalParameterById(int id)
        {
            List<CTL_AdditionalParameterDE> list = new List<CTL_AdditionalParameterDE>();
            list = _ctlSvc.SearchCTL_AdditionalParameter(new CTL_AdditionalParameterDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostCTL_AdditionalParameter(CTL_AdditionalParameterDE AdditionalParameter)
        {
            AdditionalParameter.DBoperation = DBoperations.Insert;
            _ctlSvc.ManageCTL_AdditionalParameter(AdditionalParameter);
            return Ok(AdditionalParameter);
        }

        [HttpPut]
        public IActionResult PutCustomer(CTL_AdditionalParameterDE AdditionalParameter)
        {
            AdditionalParameter.DBoperation = DBoperations.Update;
            _ctlSvc.ManageCTL_AdditionalParameter(AdditionalParameter);
            return Ok(AdditionalParameter);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCTL_AdditionalParameter(int id)
        {
            CTL_AdditionalParameterDE CTL_AdditionalParameterDe = new CTL_AdditionalParameterDE();
            CTL_AdditionalParameterDe.DBoperation = DBoperations.Delete;
            CTL_AdditionalParameterDe.Id = id;
            _ctlSvc.ManageCTL_AdditionalParameter(CTL_AdditionalParameterDe);
            return Ok();
        }

    }
}
