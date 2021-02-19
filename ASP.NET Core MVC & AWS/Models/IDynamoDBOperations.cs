using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irisi_Bruno_lab3.Models
{
    public interface IDynamoDBOperations
    {
        void AddReview(DynamoReview review);
        List<DynamoReview> GetAllDynamoReview();
    }
}
