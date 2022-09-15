using System;
using System.Collections.Generic;

#nullable disable

namespace QLCT.DAL.Models
{
    public partial class User
    {
        public User()
        {
            BelongTos = new HashSet<BelongTo>();
            Groups = new HashSet<Group>();
            IncomeOrSpendings = new HashSet<IncomeOrSpending>();
            UserToGroups = new HashSet<UserToGroup>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Active { get; set; }
        public string Role { get; set; }

        public virtual ICollection<BelongTo> BelongTos { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<IncomeOrSpending> IncomeOrSpendings { get; set; }
        public virtual ICollection<UserToGroup> UserToGroups { get; set; }
    }
}
