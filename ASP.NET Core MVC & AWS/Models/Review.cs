using System;
using System.Collections.Generic;

namespace Irisi_Bruno_lab3.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string UserName { get; set; }
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Users UserNameNavigation { get; set; }
    }
}
