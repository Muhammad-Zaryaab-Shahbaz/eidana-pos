using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private PageService _PageSvc;
        public PageController()
        {
            _PageSvc = new PageService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetPage()
        {
            List<PageDE> list = new List<PageDE>();
            list = _PageSvc.SearchPage(new PageDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchPage(PageDE Page)
        {
            List<PageDE> list = _PageSvc.SearchPage(Page);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetPageById(int id)
        {
            List<PageDE> list = new List<PageDE>();
            list = _PageSvc.SearchPage(new PageDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostPage(PageDE Page)
        {
            Page.DBoperation = DBoperations.Insert;
            _PageSvc.ManagePage(Page);
            return Ok();
        }

        [HttpPut]
        public IActionResult PutPage(PageDE Page)
        {
            Page.DBoperation = DBoperations.Update;
            _PageSvc.ManagePage(Page);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePage(int id)
        {
            PageDE PageDe = new PageDE();
            PageDe.DBoperation = DBoperations.Delete;
            PageDe.Id = id;
            _PageSvc.ManagePage(PageDe);
            return Ok();
        }
    }
}
