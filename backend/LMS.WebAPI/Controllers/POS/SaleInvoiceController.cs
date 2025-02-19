using LMS.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using LMS.Core.Entities.POS;
using LMS.Service.POS;
using LMS.DAL.POS;

namespace LMS.WebAPI.Controllers.POS
{
        [Route("api/[controller]")]
        [ApiController]

        public class saleinvoiceController : ControllerBase
        {
            public readonly ISaleService _poSVC;

            public saleinvoiceController(ISaleService PoSvc)
            {
                _poSVC = PoSvc;
            }


            #region Http Verbs



            [HttpPost("next-InvoNo-number")]
            public async Task<IActionResult> GetNextInvoNo([FromBody] SaleInvoiceDE Si)
            {
                if (Si == null)
                    return BadRequest("Invalid search criteria.");

                try
                {
                    Si.InvoiceNo = await Task.Run(() => _poSVC.GetNextInvoNo(Si));
                    return Ok(Si);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Internal server error.");
                }
            }
            [HttpGet]
            public async Task<IActionResult> Get()
            {
                try
                {
                    SaleInvoiceDE SI = new SaleInvoiceDE();
                    List<SaleInvoiceDE> list = await Task.Run(() => _poSVC.POS_Search_SaleInvoice(SI));
                    return Ok(list);
                }
                catch (Exception ex)
                {

                    return StatusCode(500, "Internal server error.");
                }
            }
            [HttpGet("{id}")]
            public async Task<IActionResult> GetInvoiceById(int id)
            {
                if (id <= 0)
                    return BadRequest("Invalid ID.");

                try
                {
                    SaleInvoiceDE SI = new SaleInvoiceDE { Id = id };
                    List<SaleInvoiceDE> list = await Task.Run(() => _poSVC.POS_Search_SaleInvoice(SI));
                    if (list.Count == 0)
                        return NotFound("Record not found.");

                    return Ok(list);
                }
                catch (Exception ex)
                {

                    return StatusCode(500, "Internal server error.");
                }
            }

            [HttpPost("Search")]
            public async Task<IActionResult> Search([FromBody] SaleInvoiceDE search)
            {
                if (search == null)
                    return BadRequest("Invalid search criteria.");

                try
                {
                    List<SaleInvoiceDE> list = await Task.Run(() => _poSVC.POS_Search_SaleInvoice(search));
                    return Ok(list);
                }
                catch (Exception ex)
                {

                    return StatusCode(500, "Internal server error.");
                }
            }
            [HttpPost]
            public async Task<IActionResult> Post([FromBody] SaleInvoiceDE preturn)
            {
                if (preturn == null)
                    return BadRequest("Invalid SaleInvoiceDE record.");

                try
                {
                    preturn.DBoperation = DBoperations.Insert;
                    SaleInvoiceDE SI = await Task.Run(() => _poSVC.POS_Manage_SaleInvoice(preturn));
                    return CreatedAtAction(nameof(GetInvoiceById), new { id = SI.Id }, SI);
                }
                catch (Exception ex)
                {

                    return StatusCode(500, "Internal server error.");
                }
            }

            [HttpPut]
            public async Task<IActionResult> Put([FromBody] SaleInvoiceDE preturn)
            {
                if (preturn == null)
                    return BadRequest("Invalid SaleInvoiceDE record.");

                try
                {
                    preturn.DBoperation = DBoperations.Update;
                    SaleInvoiceDE SI = await Task.Run(() => _poSVC.POS_Manage_SaleInvoice(preturn));
                    return Ok(SI);
                }
                catch (Exception ex)
                {

                    return StatusCode(500, "Internal server error.");
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                if (id <= 0)
                    return BadRequest("Invalid ID.");

                try
                {
                    SaleInvoiceDE preturn = new SaleInvoiceDE { Id = id, DBoperation = DBoperations.Delete };
                    SaleInvoiceDE SI = await Task.Run(() => _poSVC.POS_Manage_SaleInvoice(preturn));
                    return Ok(SI);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error.");
                }
            }

            #endregion
        }
    }
