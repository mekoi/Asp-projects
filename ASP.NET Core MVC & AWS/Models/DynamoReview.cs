using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace Irisi_Bruno_lab3.Models
{   
        [DynamoDBTable("Reviews")]
        public class DynamoReview
        {
            public int Review_Id { get; set; }
            public int Movie_Id { get; set; }
            public string Review_Text { get; set; }
            public string UserName { get; set; }     
            public DateTime ReviewMadeAt { get; set; }
            public int Stars { get; set; }
        }
    }