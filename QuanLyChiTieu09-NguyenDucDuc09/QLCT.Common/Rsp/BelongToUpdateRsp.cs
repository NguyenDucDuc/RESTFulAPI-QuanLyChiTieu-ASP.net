using System;
using System.Collections.Generic;
using System.Text;

namespace QLCT.Common.Rsp
{
    public class BelongToUpdateRsp
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }

        public virtual GroupCreateRsp Group { get; set; }
        public virtual UserRsp User { get; set; }
    }
}
