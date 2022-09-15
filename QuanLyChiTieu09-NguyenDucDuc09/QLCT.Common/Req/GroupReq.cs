using System;
using System.Collections.Generic;
using System.Text;

namespace QLCT.Common.Req
{
    public class GroupReq
    {
        public int Id { get; set; }
        public string Groupname { get; set; }
        public int? UserId { get; set; }
    }
}
