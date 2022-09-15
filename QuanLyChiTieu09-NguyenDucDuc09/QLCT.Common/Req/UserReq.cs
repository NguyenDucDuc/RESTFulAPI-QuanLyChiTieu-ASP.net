using System;
using System.Collections.Generic;
using System.Text;

namespace QLCT.Common.Req
{
    public class UserReq
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Active { get; set; }
        public string Role { get; set; }
    }
}
