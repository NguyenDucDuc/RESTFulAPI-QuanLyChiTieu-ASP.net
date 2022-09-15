using System;
using System.Collections.Generic;
using System.Text;

namespace QLCT.Common.Req
{
    public class IncomeOrSpendingUpdateReq
    {

        public decimal? Money { get; set; }
        public string Purpose { get; set; }
        public string Type { get; set; }
        public DateTime? Time { get; set; }

    }
}
