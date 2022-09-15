using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLCT.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLCT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private IncomeOrSpendingSvc incomeOrSpendingSvc;
        private UserToGroupSvc userToGroupSvc;
        public StatsController()
        {
            incomeOrSpendingSvc = new IncomeOrSpendingSvc();
            userToGroupSvc = new UserToGroupSvc();
        }

        [HttpGet("total-stats/{userId}")]
        public IActionResult TotalStats([FromRoute] int userId)
        {
            var res = incomeOrSpendingSvc.TotalStats(userId);
            return Ok(res);
        }

        [HttpGet("have-to-pay/{groupId}")]
        public IActionResult HaveToPay([FromRoute] int groupId)
        {
            var res = userToGroupSvc.HaveToPay(groupId);
            return Ok(res);
        }
    }
}
