using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChatroomBusiness.Repositories;
using Serilog;
using ChatroomBusiness.EF;

namespace ChatRoom.Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly UserService _service;

        public UserController()
        {
            _service = new UserService();
        }
        [Route("")]
        public IHttpActionResult Get()
        {
            var user = _service.GetAllUsers();
            return Ok(user);
        }
        [Route("{id}")]
        public IHttpActionResult GetUsersbyId(string id)
        {
            try
            {
                
                var userid = _service.GetUsersById(id);
                if(userid == null)
                {
                    return BadRequest($"Cannot find User with id {id}");
                }
                return Ok(userid);

            }
            catch (Exception ex)
            {
                Log.Error($"An error occured while trying to retrieve payload {ex.Message}");
                return BadRequest($"An error occured while trying to retrieve payload {ex.Message}");


            }
            
        }
       
        [Route("")]
        public IHttpActionResult Post(Users model)
        {
            try
            {
                var userexist = _service.GetAllUsers().Where(x =>x.UserName.ToLower() == model.UserName.ToLower()).FirstOrDefault();
                if(userexist != null)
                {
                    Log.Error($"Users, There is an existing user with the username {model.UserName}");
                    return BadRequest($"There is an existing user with the username {model.UserName}");
                }
                var newuser = _service.CreateUsers(model);
                return Ok($"User {model.UserName} successfully created ");
            }
            catch (Exception ex)
            {

                Log.Error($"An error occured while trying to create user {ex.Message} ");
                return BadRequest($"An error occured while trying to create user {ex.Message}");
            }
        }
        [Route("")]
        public IHttpActionResult Put(Users model)
        {
            try
            {
                var usersexist = _service.GetUsersById(model.Id);
                if(usersexist == null)
                {
                    Log.Error($"Users {model.UserName} does not exist");
                    return BadRequest($"User {model.UserName} does not exist");
                }
                var updateuser = _service.UpdateUsers(model);
                return Ok($"User successfully updated");
            }
            catch (Exception ex)
            {

                Log.Error($"An error occured while trying to perform Update Operation {ex.Message}");
                return BadRequest();
            }
        }
        [Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                var userid = _service.GetUsersById(id);
                if (userid == null)
                {
                    return BadRequest($"Cannot find User with id {id}");
                }
                _service.DeleteUser(id);
                return Ok($"User with id {id} successfully deleted");
            }
            catch (Exception ex)
            {

                Log.Error($"An error occured while trying to delete user {ex.Message}");
                return BadRequest();
            }
        }
    }
}
