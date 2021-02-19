using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;

namespace Irisi_Bruno_lab3.Models
{
    public interface IS3Processes
    {
        BasicAWSCredentials GetBasicCredentials();
        void GetItemsinBucket();
    }
}
