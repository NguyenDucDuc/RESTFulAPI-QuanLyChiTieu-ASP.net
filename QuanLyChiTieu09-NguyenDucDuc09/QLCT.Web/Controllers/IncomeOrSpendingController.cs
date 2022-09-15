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
    public class IncomeOrSpendingController : ControllerBase
    {
        private IncomeOrSpendingSvc incomeOrSpendingSvc;
        private UserSvc userSvc;
        public IncomeOrSpendingController()
        {
            incomeOrSpendingSvc = new IncomeOrSpendingSvc();
            userSvc = new UserSvc();
        }

        [HttpPost("create-income-or-spending")]
        public IActionResult Create([FromBody] IncomeOrSpendingCreateReq incomeOrSpendingCreateReq, [FromHeader] string token)
        {
            if (token != null)
            {
                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));
                if (currentUser != null)
                {
                    var res = incomeOrSpendingSvc.Create(incomeOrSpendingCreateReq, currentUser.Id);
                    if (res != null)
                        return Ok(res);
                }
            }
            DeleteRsp error = new DeleteRsp();
            error.StatusCode = 403;
            error.Message = "Access denied";
            return NotFound(error);
        }

        [HttpPost("update-income-or-spending/{id}")]
        public IActionResult Update([FromBody] IncomeOrSpendingUpdateReq incomeOrSpendingUpdateReq, [FromRoute] int id, [FromHeader] string token)
        {

            if (token != null)
            {
                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));
                // lay thu chi tu route id
                IncomeOrSpending incomeOrSpending = incomeOrSpendingSvc.GetById(id);
                if (currentUser.Role == "ADMIN" || currentUser.Id == incomeOrSpending.UserId)
                {
                    var res = incomeOrSpendingSvc.Update(incomeOrSpendingUpdateReq, id, currentUser.Id);
                    if (res != null)
                        return Ok(res);
                }
            }

            DeleteRsp error = new DeleteRsp();
            error.StatusCode = 403;
            error.Message = "Access denied";
            return NotFound(error);
        }

        [HttpGet("get-detail/{id}")]
        public IActionResult Detail([FromRoute] int id)
        {
            var res = incomeOrSpendingSvc.Detail(id);
            return Ok(res);
        }

        [HttpGet("get-all-income/{userId}")]
        public IActionResult GetAllIncome([FromRoute] int userId)
        {
            var res = incomeOrSpendingSvc.GetAllIncome(userId);
            return Ok(res);
        }

        [HttpGet("get-all-spending/{userId}")]
        public IActionResult GetAllSpending([FromRoute] int userId)
        {
            var res = incomeOrSpendingSvc.GetAllSpending(userId);
            return Ok(res);
        }

        [HttpDelete("delete-income-or-spending/{id}")]
        public IActionResult Delete([FromRoute] int id, [FromHeader] string token)
        {
            if (token != null)
            {
                //lay thong tin user tu token
                string currentUserId = token.Split("`")[1];
                User currentUser = userSvc.GetById(int.Parse(currentUserId));

                //lay thong tin user tao ra income_or_spending tu incomespending id
                IncomeOrSpending incomeOrSpending = incomeOrSpendingSvc.GetById(id);

                if (currentUser.Role == "ADMIN" || currentUser.Id == incomeOrSpending.UserId)
                {
                    var res = incomeOrSpendingSvc.Delete(id);
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
