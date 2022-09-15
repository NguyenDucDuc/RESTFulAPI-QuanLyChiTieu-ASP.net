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
    public class UserToGroupController : ControllerBase
    {
        private UserSvc userSvc;
        private UserToGroupSvc userToGroupSvc;
        private BelongToSvc belongToSvc;
        public UserToGroupController()
        {
            userSvc = new UserSvc();
            userToGroupSvc = new UserToGroupSvc();
            belongToSvc = new BelongToSvc();

        }


        [HttpPost("create")]
        public IActionResult Create([FromHeader] string token, [FromBody] UserToGroupCreateReq userToGroupCreateReq)
        {
            if (token != null)
            {
                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));

                //Lay thong tin xem user co thuoc ve nhom nay khong, neu thuoc voi cho chi

                BelongTo belong = belongToSvc.GetByUserIdAndGroupId(currentUser.Id, (int)userToGroupCreateReq.GroupId);
                if (currentUser != null && belong != null)
                {
                    var res = userToGroupSvc.Create(userToGroupCreateReq, currentUser.Id);
                    if (res != null)
                        return Ok(res);
                }
            }
            DeleteRsp error = new DeleteRsp();
            error.StatusCode = 403;
            error.Message = "Access denied";
            return NotFound(error);
        }

        [HttpGet("get-all/{userId}")]
        public IActionResult GetAll([FromRoute] int userId)
        {
            var res = userToGroupSvc.GetAll(userId);
            return Ok(res);
        }

        [HttpPut("update-money/{userToGroupId}")]
        public IActionResult Update([FromRoute] int userToGroupId, [FromBody] decimal money, [FromHeader] string token)
        {
            if (token != null)
            {
                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));
                //lay thong tin thu chi nhom va so sanh xem user update co phai la user tao ra thu chi do khong, neu co thi cho phep
                UserToGroup userToGroup = userToGroupSvc.GetById(userToGroupId);
                if (currentUser.Role == "ADMIN" || currentUser.Id == userToGroup.UserId)
                {
                    var res = userToGroupSvc.Update(userToGroupId, money);
                    if (res != null)
                        return Ok(res);
                }
            }
            DeleteRsp error = new DeleteRsp();
            error.StatusCode = 403;
            error.Message = "Access denied";
            return NotFound(error);
        }

        [HttpDelete("delete/{userToGroupId}")]
        public IActionResult Delete([FromRoute] int userToGroupId, [FromHeader] string token)
        {
            if (token != null)
            {
                //lay thong tin user tu token
                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));

                //lay thong tin user tao ra income_or_spending tu incomespending id
                UserToGroup userToGroup = userToGroupSvc.GetById(userToGroupId);

                if (currentUser.Role == "ADMIN" || currentUser.Id == userToGroup.UserId)
                {
                    var res = userToGroupSvc.Delete(userToGroupId);
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
