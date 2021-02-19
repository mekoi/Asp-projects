using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System.Threading;

namespace Irisi_Bruno_lab3.Models
{
    public class S3Processes : IS3Processes
    {
        public S3Processes() 
        {

        }
        public BasicAWSCredentials GetBasicCredentials()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("SecretAccesskey").Value;

            return new BasicAWSCredentials(accessKeyID, secretKey);
        }

        public void GetItemsinBucket()
        {

        }
    }
}
