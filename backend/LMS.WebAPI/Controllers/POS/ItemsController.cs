using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS.Core.Entities.POS;
using LMS.Service.POS;


namespace LMS.WebAPI.Controllers.POS

{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public readonly ISaleService _pslSvc;
        public ItemsController(ISaleService pslSvc)
        {
            _pslSvc = pslSvc;
        }

        // HTTP Methods 
        [HttpGet]
        public IActionResult GetItems()
        {
            List<ItemsDE> list = new List<ItemsDE>();
            list = _pslSvc.Search_Items(new ItemsDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchItems(ItemsDE item)
        {
            List<ItemsDE> list = _pslSvc.Search_Items(item);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetItemsbyId(int id) 
        {
            List<ItemsDE> list = new List<ItemsDE>();
            list = _pslSvc.Search_Items(new ItemsDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostItems(ItemsDE item)
        {
            item.DBoperation = DBoperations.Insert;
            _pslSvc.Manage_Items(item);
            return Ok(item);
        }

        [HttpPut]
        public IActionResult PutItems(ItemsDE item)
        {
            item.DBoperation = DBoperations.Update;
            _pslSvc.Manage_Items(item);
            return Ok(item);
        }

        [HttpDelete("{id}")] 
        public IActionResult DeleteItems(int id) 
        {
            ItemsDE ItemsDE = new ItemsDE();
            ItemsDE.DBoperation = DBoperations.Delete;
            ItemsDE.Id = id;
            _pslSvc.Manage_Items(ItemsDE);
            return Ok();
        }

    }
}
