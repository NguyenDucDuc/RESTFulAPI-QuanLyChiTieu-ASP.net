using System;
using System.Collections.Generic;
using System.Text;

namespace QLCT.Common.Rsp
{
    public class UserRsp
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Active { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
    }
}
