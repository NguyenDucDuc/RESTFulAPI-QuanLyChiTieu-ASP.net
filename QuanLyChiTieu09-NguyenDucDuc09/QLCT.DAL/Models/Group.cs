using System;
using System.Collections.Generic;

#nullable disable

namespace QLCT.DAL.Models
{
    public partial class Group
    {
        public Group()
        {
            BelongTos = new HashSet<BelongTo>();
            UserToGroups = new HashSet<UserToGroup>();
        }

        public int Id { get; set; }
        public string Groupname { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<BelongTo> BelongTos { get; set; }
        public virtual ICollection<UserToGroup> UserToGroups { get; set; }
    }
}
