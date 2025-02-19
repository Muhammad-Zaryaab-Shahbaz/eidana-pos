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
    public class InvoiceController : ControllerBase 
    {
        public readonly IRcvService _rcvSvc;
        public InvoiceController(IRcvService rcvSvc)
        {
            _rcvSvc = rcvSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetInvoice()
        {
            List<InvoiceDE> list = new List<InvoiceDE>();
            list = _rcvSvc.SearchInvoice(new InvoiceDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchInvoice(InvoiceDE Invoice)
        {
            List<InvoiceDE> list = _rcvSvc.SearchInvoice(Invoice);
            return Ok(list);
        }

        [HttpPost("Invoice")]
        public IActionResult GetInvoice(InvoiceDE Invoice)
        {
            List<InvoiceDE> list = _rcvSvc.SearchInvoice(Invoice);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoiceById(int id)
        {
            List<InvoiceDE> list = new List<InvoiceDE>();
            list = _rcvSvc.SearchInvoice(new InvoiceDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostInvoice(InvoiceDE Invoice)
        {
            Invoice.DBoperation = DBoperations.Insert;
             _rcvSvc.ManageInvoice(Invoice);
            return Ok(Invoice);
        }

        [HttpPut]
        public IActionResult PutInvoice(InvoiceDE Invoice)
        {
            Invoice.DBoperation = DBoperations.Update;
            _rcvSvc.ManageInvoice(Invoice);
            return Ok(Invoice);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            InvoiceDE InvoiceDe = new InvoiceDE();
            InvoiceDe.DBoperation = DBoperations.Delete;
            InvoiceDe.Id = id;
            _rcvSvc.ManageInvoice(InvoiceDe);
            return Ok();
        }
        
    }
}
