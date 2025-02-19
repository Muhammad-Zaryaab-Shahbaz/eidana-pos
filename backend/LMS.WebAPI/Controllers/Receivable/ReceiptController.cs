using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS.Core.Entities.Receivable;
using LMS.Service.Receivable;


namespace LMS.WebAPI.Receivable.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        public readonly IRcvService _rcvSvc;
        public ReceiptController(IRcvService rcvSvc)
        {
            _rcvSvc = rcvSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetReceipt()
        {
            List<ReceiptDE> list = new List<ReceiptDE>();
            list = _rcvSvc.SearchReceipt(new ReceiptDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchReceipt(ReceiptDE Receipt)
        {
            List<ReceiptDE> list = _rcvSvc.SearchReceipt(Receipt);
            return Ok(list);
        }

        [HttpPost("Receipt")]
        public IActionResult GetReceipt(ReceiptDE Receipt)
        {
            List<ReceiptDE> list = _rcvSvc.SearchReceipt(Receipt);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetReceiptById(int id)
        {
            List<ReceiptDE> list = new List<ReceiptDE>();
            list = _rcvSvc.SearchReceipt(new ReceiptDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostReceipt(ReceiptDE Receipt)
        {
            Receipt.DBoperation = DBoperations.Insert;
            _rcvSvc.ManageReceipt(Receipt);
            return Ok(Receipt);
        }

        [HttpPut]
        public IActionResult PutReceipt(ReceiptDE Receipt)
        {
            Receipt.DBoperation = DBoperations.Update;
            _rcvSvc.ManageReceipt(Receipt);
            return Ok(Receipt);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReceipt(int id)
        {
            ReceiptDE ReceiptDe = new ReceiptDE();
            ReceiptDe.DBoperation = DBoperations.Delete;
            ReceiptDe.Id = id;
            _rcvSvc.ManageReceipt(ReceiptDe);
            return Ok();
        }

    }
}
