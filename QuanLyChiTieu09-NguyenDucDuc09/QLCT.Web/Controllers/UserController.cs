using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLCT.BLL;
using QLCT.Common.Req;
using QLCT.Common.Rsp;
using QLCT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLCT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserSvc userSvc;
        public UserController()
        {
            userSvc = new UserSvc();
        }
        [HttpPost("create-user")]
        public IActionResult CreateUser([FromBody] UserReq userReq)
        {
            var res = userSvc.CreateUser(userReq);
            return Ok(res);
        }

        [HttpGet("get-all-user")]
        public IActionResult GetAllUser()
        {
            var res = userSvc.GetAll();
            return Ok(res);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginReq userLoginReq)
        {
            var res = userSvc.Login(userLoginReq);
            return Ok(res);
        }

        [HttpPut("update-profile/{userId}")]
        public IActionResult Update([FromRoute] int userId, [FromBody] UserUpdateReq userUpdateReq, [FromHeader] string token)
        {
            if (token != null)
            {

                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));
                if (currentUser.Role == "ADMIN" || currentUser.Id == userId)
                {
                    var res = userSvc.Update(userId, userUpdateReq);
                    if (res != null)
                        return Ok(res);

                }
            }
            DeleteRsp error = new DeleteRsp();
            error.StatusCode = 401;
            error.Message = "UnAuthorization";
            return NotFound(error);

        }
    }
}
