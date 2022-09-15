using QLCT.Common.Req;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLCT.Common.Rsp
{
    public class IncomeOrSpendingDetailRsp
    {
        public int Id { get; set; }
        public decimal? Money { get; set; }
        public string Purpose { get; set; }
        public string Type { get; set; }
        public DateTime? Time { get; set; }
        public int? UserId { get; set; }
        public virtual UserReq User { get; set; }

    }
}
