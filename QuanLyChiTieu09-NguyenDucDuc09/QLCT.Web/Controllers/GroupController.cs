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
    public class GroupController : ControllerBase
    {
        private GroupSvc groupSvc;
        private UserSvc userSvc;
        public GroupController()
        {
            groupSvc = new GroupSvc();
            userSvc = new UserSvc();
        }

        [HttpPost("/create")]
        public IActionResult Create([FromBody] GroupCreateReq groupCreateReq, [FromHeader] string token)
        {
            if (token != null)
            {
                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));
                if (currentUser != null)
                {
                    var res = groupSvc.Create(groupCreateReq, currentUser.Id);
                    if (res != null)
                        return Ok(res);
                }
            }
            DeleteRsp error = new DeleteRsp();
            error.StatusCode = 403;
            error.Message = "Access denied";
            return NotFound(error);


        }

        [HttpPut("/update/{groupId}")]
        public IActionResult Update([FromRoute] int groupId, [FromBody] string groupName, [FromHeader] string token)
        {

            if (token != null)
            {
                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));
                //Lay thong tin group tu groupId
                Group group = groupSvc.GetById(groupId);

                if (currentUser.Role == "ADMIN" || currentUser.Id == group.UserId)
                {
                    var res = groupSvc.Update(groupName, groupId, currentUser.Id);
                    if (res != null)
                        return Ok(res);
                }
            }
            DeleteRsp error = new DeleteRsp();
            error.StatusCode = 403;
            error.Message = "Access denied";
            return NotFound(error);
        }

        [HttpGet("/get-all/{userId}")]
        public IActionResult GetAll([FromRoute] int userId)
        {
            var res = groupSvc.GetAll(userId);
            return Ok(res);
        }

        [HttpDelete("/delete/{groupId}")]
        public IActionResult Delete([FromRoute] int groupId, [FromHeader] string token)
        {

            if (token != null)
            {
                //lay thong tin user tu token
                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));

                //lay thong tin user tao ra group tu group id
                Group group = groupSvc.GetById(groupId);

                if (currentUser.Role == "ADMIN" || currentUser.Id == group.UserId)
                {
                    var res = groupSvc.Delete(groupId);
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
