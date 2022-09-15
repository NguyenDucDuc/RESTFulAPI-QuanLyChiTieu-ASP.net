using System;
using System.Collections.Generic;

#nullable disable

namespace QLCT.DAL.Models
{
    public partial class UserToGroup
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public decimal? Money { get; set; }
        public DateTime? Time { get; set; }
        public string Purpose { get; set; }
        public string Type { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
