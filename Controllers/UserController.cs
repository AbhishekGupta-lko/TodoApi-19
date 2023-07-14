using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TodoApi.Cache;
using TodoApi.DAL;
using TodoApi.Models;
using Microsoft.Extensions.Hosting;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbContextClass _context;
        private readonly ICacheService _cacheService;
        private readonly UserDAL _userDAL=new UserDAL ();

        public UserController(DbContextClass context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        //[Authorize]
        [HttpGet]
        [Route("UsersList")]
        public async Task<IActionResult> Get() 
        {
            try
            {
                var user = await _userDAL.fn_GetAllUsers("Get");
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("UserDetail")]
        public async Task<IActionResult> Get(int? UserID)
        {
           
            if (UserID == null)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userDAL.fn_GetAllUsersByID("GetByID", UserID);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> POST([FromBody] User user)
        {
                try
                {
                    User _newUser = new User();
                    _newUser = await _userDAL.fn_InsertUser("Post", user);
                    if (!string.IsNullOrEmpty(_newUser.ResponseMsg))
                    {
                        return Ok(_newUser);
                    }
                    else
                    {
                        return BadRequest("Invalid Request");
                    }
                }
                catch (Exception)
                {
                    return BadRequest("Invalid Request");
                }
        }

        [HttpPost]
        [Route("DeleteUser")]
        public async Task<IActionResult> Delete(int? UserID)
        {
            if (UserID == null)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userDAL.fn_DeleteUser("Delete", UserID);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            try
            {
                User _newUser = new User();
                _newUser = await _userDAL.fn_UpdateUser("Update", user);
                if (!string.IsNullOrEmpty(_newUser.ResponseMsg))
                {
                    return Ok(_newUser);
                }
                else
                {
                    return BadRequest("Invalid Request");
                }
            }
            catch (Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
    }
}
