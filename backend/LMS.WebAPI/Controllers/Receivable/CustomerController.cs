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
    public class CustomerController : ControllerBase
    {
        public readonly IRcvService _rcvSvc;
        public CustomerController(IRcvService rcvSvc)
        {
            _rcvSvc = rcvSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetCustomer()
        {
            List<CustomerDE> list = new List<CustomerDE>();
            list = _rcvSvc.SearchCustomer(new CustomerDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchCustomer(CustomerDE Customer)
        {
            List<CustomerDE> list = _rcvSvc.SearchCustomer(Customer);
            return Ok(list);
        }

        [HttpPost("Customer")]
        public IActionResult GetCustomer(CustomerDE Customer)
        {
            List<CustomerDE> list = _rcvSvc.SearchCustomer(Customer);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            List<CustomerDE> list = new List<CustomerDE>();
            list = _rcvSvc.SearchCustomer(new CustomerDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostCustomer(CustomerDE Customer)
        {
            Customer.DBoperation = DBoperations.Insert;
            _rcvSvc.ManageCustomer(Customer);
            return Ok(Customer);
        }

        [HttpPut]
        public IActionResult PutCustomer(CustomerDE Customer)
        {
            Customer.DBoperation = DBoperations.Update;
            _rcvSvc.ManageCustomer(Customer);
            return Ok(Customer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            CustomerDE CustomerDe = new CustomerDE();
            CustomerDe.DBoperation = DBoperations.Delete;
            CustomerDe.Id = id;
            _rcvSvc.ManageCustomer(CustomerDe);
            return Ok();
        }

    }
}
