﻿using LMS.Core.Entities;
using LMS.Core.Enums;
using LMS.Core.SearchCriteria;
using LMS.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private UserTaskService _tskSvc;
        public UserTaskController()
        {
            _tskSvc = new UserTaskService();
        }
        // HTTP Methods 
        [HttpGet]
        public ActionResult Get()
        {
            TaskSearchCriteria sc = new TaskSearchCriteria();
            List<UserTaskDE> values = _tskSvc.GetTodaysTasks(sc);

            // Filter tasks to include only those with a UserId
            List<UserTaskDE> filteredValues = values.Where(task => !string.IsNullOrEmpty(task.UserId)).ToList();

            return Ok(filteredValues);
        }

        [HttpGet("{id}")]
        public IActionResult GetusertaskById(int id)
        {
            List<UserTaskDE> list = new List<UserTaskDE>();
            TaskSearchCriteria sc = new TaskSearchCriteria();
            sc.Id = id;
            list = _tskSvc.GetTodaysTasks(sc);
            return Ok(list[0]);

        }

        [HttpPost ("{Search}")]
        public ActionResult Search ( TaskSearchCriteria usr )
        {
            //assignTask.IsActive = true;
            List<UserTaskDE> values = _tskSvc.Searchusertask (usr);
            return Ok (values);
        }
        [HttpPost]
        public ActionResult Post(List<UserTaskDE> mod)
        {
            foreach (var item in mod)
                item.DBoperation = DBoperations.Insert;
            bool usr = _tskSvc.Manageusertask(mod);
            return Ok(usr);
        }
        [HttpPut]
        public ActionResult Put(List<UserTaskDE> tasks)
        {
            foreach (var item in tasks)
                item.DBoperation = DBoperations.Update;

            bool retVal = _tskSvc.Manageusertask(tasks);

            if (retVal)
            {
                return Ok(tasks);
            }
            else
            {
                // Handle the case where the update operation failed
                return BadRequest("Failed to update tasks");
            }
        }

        /*public ActionResult Put(UserTaskDE mod)
        {
            List<UserTaskDE> tasks = new List<UserTaskDE>();
            tasks.Add(mod);
            foreach(var item in tasks)
            item.DBoperation = DBoperations.Update;
            bool retVal = _tskSvc.Manageusertask(tasks);
            //if(!retVal)
                
            return Ok(mod);
        }*/
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    UserTaskDE usr = new UserTaskDE { Id = id, DBoperation = DBoperations.Delete };
        //    _tskSvc.Manageusertask(usr);
        //}
        /*[HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            UserTaskDE patDE = new UserTaskDE();
            patDE.DBoperation = DBoperations.Delete;
            patDE.Id = id;
            _tskSvc.Manageusertask(patDE);
            return Ok();
        }*/
        //[HttpDelete("{id}")]
        //public IActionResult DeleteUsertask(int id)
        //{
        //    UserTaskDE patDE = new UserTaskDE();
        //    patDE.DBoperation = DBoperations.Delete;
        //    patDE.Id = id;
        //    _tskSvc.Manageusertask(patDE);
        //    return Ok();
        //}
    }
}
