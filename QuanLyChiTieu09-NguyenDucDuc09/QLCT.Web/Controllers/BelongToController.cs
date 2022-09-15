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
    public class BelongToController : ControllerBase
    {
        private BelongToSvc belongToSvc;
        private UserSvc userSvc;
        private GroupSvc groupSvc;
        public BelongToController()
        {
            belongToSvc = new BelongToSvc();
            userSvc = new UserSvc();
            groupSvc = new GroupSvc();
        }

        [HttpPost("add-user-to-group")]
        public IActionResult AddUserToGroup([FromBody] BelongToReq belongToReq, [FromHeader] string token)
        {

            if (token != null)
            {
                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));
                Group group = groupSvc.GetById((int)belongToReq.GroupId);
                if (currentUser.Role == "ADMIN" || currentUser.Id == group.UserId)
                {
                    var res = belongToSvc.Create(belongToReq);
                    if (res != null)
                    {
                        DeleteRsp success = new DeleteRsp();
                        success.StatusCode = 201;
                        success.Message = "Add success";
                        return Ok(success);
                    }
                }
            }
            DeleteRsp error = new DeleteRsp();
            error.StatusCode = 500;
            error.Message = "Add failed";
            return NotFound(error);

        }

        [HttpPut("update/{belongToId}")]
        public IActionResult Update([FromRoute] int belongToId, [FromBody] BelongToUpdateReq belongToUpdateReq)
        {
            var res = belongToSvc.Update(belongToId, belongToUpdateReq);
            if (res != null)
            {
                DeleteRsp success = new DeleteRsp();
                success.StatusCode = 200;
                success.Message = "Update success";
                return Ok(success);
            }
            DeleteRsp error = new DeleteRsp();
            error.StatusCode = 500;
            error.Message = "Update failed";
            return NotFound(error);
        }

    }
}
