﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QLCT.Common.Req
{
    public class BelongToReq
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
    }
}
