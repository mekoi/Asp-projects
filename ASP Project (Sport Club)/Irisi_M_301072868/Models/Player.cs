using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Irisi_M_301072868.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DayOfBirth { get; set; }

        public String Gender { get; set; }

        public String PhoneNo { get; set; }

        public String Email { get; set; }

        public int ClubId { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ClubJoinedDate { get; set; }

        public virtual Club clubs { get; set; }
    }
}
