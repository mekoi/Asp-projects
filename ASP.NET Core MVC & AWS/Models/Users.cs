using System;
using System.Collections.Generic;

namespace Irisi_Bruno_lab3.Models
{
    public partial class Users
    {
        public Users()
        {
            Review = new HashSet<Review>();
        }

        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public virtual ICollection<Review> Review { get; set; }
    }
}
