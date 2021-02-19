using System;
using System.Collections.Generic;

namespace Irisi_Bruno_lab3.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Review = new HashSet<Review>();
        }

        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string S3UrlVideo { get; set; }
        public string S3UrlImage { get; set; }

        public virtual ICollection<Review> Review { get; set; }
    }
}
