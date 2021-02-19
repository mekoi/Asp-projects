using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Irisi_M_301072868.Models
{
    public class Club
    {
        public int ClubId { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public String ClubName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public String AgeRange { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage ="This field is required!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
    }
}
