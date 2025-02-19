
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS.Core.Entities.TMS;
using LMS.Service.TMS;


namespace LMS.WebAPI.TMS.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NotificationTemplateController  : ControllerBase
    {
        public readonly ITmsService _tmsSvc;
        public NotificationTemplateController (ITmsService tmsSvc)
        {
            _tmsSvc = tmsSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetReceipt()
        {
            List<NotificationTemplateDE> list = new List<NotificationTemplateDE>();
            list = _tmsSvc.SearchNotificationTemplate(new NotificationTemplateDE());
            return Ok(list);
        }
        [HttpPost("{Search}")]
        public IActionResult SearchNotificationTemplate(NotificationTemplateDE NotificationTemplate)
        {
            List<NotificationTemplateDE> list = _tmsSvc.SearchNotificationTemplate(NotificationTemplate);
            return Ok(list);
        }
        [HttpPost("NotificationTemplate")]
        public IActionResult GetNotificationTemplate(NotificationTemplateDE NotificationTemplate)
        {
            List<NotificationTemplateDE> list = _tmsSvc.SearchNotificationTemplate(NotificationTemplate);
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult GetNotificationTemplateById(int id)
        {
            List<NotificationTemplateDE> list = new List<NotificationTemplateDE>();
            list = _tmsSvc.SearchNotificationTemplate(new NotificationTemplateDE { Id = id });
            return Ok(list[0]);

        }
        [HttpPost]
        public IActionResult PostNotificationTemplate (NotificationTemplateDE NotificationTemplate)
        {
            NotificationTemplate.DBoperation = DBoperations.Insert;
            _tmsSvc.ManageNotificationTemplate(NotificationTemplate);
            return Ok(NotificationTemplate);
        }
        [HttpPut]
        public IActionResult PutNotificationTemplate (NotificationTemplateDE NotificationTemplate)
        {
            NotificationTemplate.DBoperation = DBoperations.Update;
            _tmsSvc.ManageNotificationTemplate(NotificationTemplate);
            return Ok(NotificationTemplate);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteNotificationTemplate (int id)
        {
            NotificationTemplateDE NotificationTemplateDe = new NotificationTemplateDE();
            NotificationTemplateDe.DBoperation = DBoperations.Delete;
            NotificationTemplateDe.Id = id;
            _tmsSvc.ManageNotificationTemplate(NotificationTemplateDe);
            return Ok();
        }
    }
}