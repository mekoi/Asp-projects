using Amazon.DynamoDBv2;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Configuration;
using System.IO;
using Irisi_Bruno_lab3.Controllers;

namespace Irisi_Bruno_lab3.Models
{
    public class DynamoDBOperations : IDynamoDBOperations
    {
        AmazonDynamoDBClient client;
        BasicAWSCredentials credentials;
        static string dynamoTableName = "Reviews";

        public DynamoDBOperations()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("SecretAccesskey").Value;

            credentials = new BasicAWSCredentials(accessKeyID, secretKey);
            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast2);
        }

        public async void AddReview(DynamoReview review)
        {
            credentials.GetCredentials();
            //var r = new DynamoReview { Review_Id = 4, Movie_Id = 1, Review_Text = "Text",  UserName = "Iris" };

            // Get Database
            DynamoDBContext context = new DynamoDBContext(client);
            
            List<DynamoReview> allDynamoReviews = GetAllDynamoReview().OrderBy(c => c.ReviewMadeAt).ToList();

            if (allDynamoReviews.Count != 0)
            {
                DynamoReview lastReview = allDynamoReviews[allDynamoReviews.Count - 1];
                review.Review_Id = lastReview.Review_Id + 1;

                if (UsersController.userActiveSession != null)
                {
                    review.UserName = UsersController.userActiveSession.UserName;
                }
                else
                {
                    review.UserName = "Unregistered User";
                }               
                review.ReviewMadeAt = DateTime.UtcNow;
                await context.SaveAsync<DynamoReview>(review);
            }
        }

        public List<DynamoReview> GetAllDynamoReview()
        {
            List<DynamoReview> allDynamoReviews = new List<DynamoReview>();
            DynamoDBContext context = new DynamoDBContext(client);

            Table table = Table.LoadTable(client, dynamoTableName);

            ScanFilter scanFilter = new ScanFilter();

            Search search = table.Scan(scanFilter);

            List<Document> documentList = new List<Document>();

            do
            {
                var taskDocumentList = search.GetNextSetAsync();
                documentList = taskDocumentList.Result;
                foreach (var document in documentList)
                {
                    DynamoReview dynamoReview = new DynamoReview();
                    foreach (var attribute in document.GetAttributeNames())
                    {
                        string stringValue = null;
                        var value = document[attribute];
                        if (value is Primitive)
                            stringValue = value.AsPrimitive().Value.ToString();
                        else if (value is PrimitiveList)
                            stringValue = string.Join(",", (from primitive in value.AsPrimitiveList().Entries select primitive.Value).ToArray());
                        if (attribute == "Review_Id")
                        {
                            dynamoReview.Review_Id = Convert.ToInt32(stringValue);
                        }
                        else if (attribute == "Movie_Id")
                        {
                            dynamoReview.Movie_Id = Convert.ToInt32(stringValue);
                        }
                        else if (attribute == "Review_Text")
                        {
                            dynamoReview.Review_Text = stringValue;
                        }
                        else if (attribute == "UserName")
                        {
                            dynamoReview.UserName = stringValue;
                        }                     
                        else if (attribute == "ReviewMadeAt")
                        {
                            dynamoReview.ReviewMadeAt = DateTime.Parse(stringValue);
                        }                      
                        else if (attribute == "Stars")
                        {
                            dynamoReview.Stars = Convert.ToInt32(stringValue);
                        }
                    }
                    allDynamoReviews.Add(dynamoReview);
                }
            } while (!search.IsDone);
            return allDynamoReviews;
        }
    }
}

