using System;
using System.Collections.Generic;
using System.Text;

namespace QLCT.Common.Rsp
{
    public class GroupCreateRsp
    {
        public int Id { get; set; }
        public string Groupname { get; set; }
        public int? UserId { get; set; }
    }
}
