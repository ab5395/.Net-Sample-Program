using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;

namespace AWSSample.Models
{

    public class Declaration
    {
        protected readonly string _accessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        protected readonly string _secretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        protected readonly string _inputBucket = ConfigurationManager.AppSettings["inputBucket"];
        protected readonly string _outputBucket = ConfigurationManager.AppSettings["outputBucket"];
        protected readonly string _pipelineId = ConfigurationManager.AppSettings["pipelineId"];
    }

    public class AmazonS3Uploader : Declaration
    {
        #region Declarations


        #endregion

        /*
        public bool sendMyFileToS3(string localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            
            // input explained :
            // localFilePath = the full local file path e.g. "c:\mydir\mysubdir\myfilename.zip"
            // bucketName : the name of the bucket in S3 ,the bucket should be alreadt created
            // subDirectoryInBucket : if this string is not empty the file will be uploaded to
            // a subdirectory with this name
            // fileNameInS3 = the file name in the S3

            // create an instance of IAmazonS3 class ,in my case i choose RegionEndpoint.EUWest1
            // you can change that to APNortheast1 , APSoutheast1 , APSoutheast2 , CNNorth1
            // SAEast1 , USEast1 , USGovCloudWest1 , USWest1 , USWest2 . this choice will not
            // store your file in a different cloud storage but (i think) it differ in performance
            // depending on your location
            IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.EUWest1);

            // create a TransferUtility instance passing it the IAmazonS3 created in the first step
            TransferUtility utility = new TransferUtility(client);
            // making a TransferUtilityUploadRequest instance
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            if (string.IsNullOrEmpty(subDirectoryInBucket))
            {
                request.BucketName = bucketName; //no subdirectory just bucket name
            }
            else
            {   // subdirectory and bucket name
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.Key = fileNameInS3; //file name up in S3
            request.FilePath = localFilePath; //local file name
            utility.Upload(request); //commensing the transfer

            return true; //indicate that the file was sent
        }
        */
        //Second Method 

        private string bucketName = "your-amazon-s3-bucket";
        private string keyName = "the-name-of-your-file";
        private string filePath = "C:\\Users\\yourUserName\\Desktop\\myImageToUpload.jpg";

        public void UploadFile()
        {
            var client = new AmazonS3Client(_accessKey, _secretKey, RegionEndpoint.USWest2);

            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    ContentType = "text/plain"
                };

                PutObjectResponse response = client.PutObject(putRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }
        }
    }

    public class AmazonS3Downloader
    {
        //Download Full File Method
        public void Download(string keyName, string bucketName)
        {
            IAmazonS3 client;
            using (client = new AmazonS3Client(RegionEndpoint.USEast1))
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                using (GetObjectResponse response = client.GetObject(request))
                {
                    string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), keyName);
                    if (!File.Exists(dest))
                    {
                        response.WriteResponseStreamToFile(dest);
                    }
                }
            }
        }

        //Instead of reading the entire object you can read only the portion of the object data by specifying the byte range in the request, as shown in the following C# code sample.
        public void DownloadFilePart(string keyName, string bucketName)
        {
            IAmazonS3 client;
            using (client = new AmazonS3Client(RegionEndpoint.USEast1))
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    ByteRange = new ByteRange(0, 10)
                };

                using (GetObjectResponse response = client.GetObject(request))
                {
                    string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), keyName);
                    if (!File.Exists(dest))
                    {
                        response.WriteResponseStreamToFile(dest);
                    }
                }
            }
        }

    }

    public class DownaloadObject
    {
        static string bucketName = "*** bucket name ***";
        static string keyName = "*** object key ***";
        static IAmazonS3 client;

        public void Download()
        {
            try
            {
                Console.WriteLine("Retrieving (GET) an object");
                string data = ReadObjectData();
            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine(s3Exception.Message,
                                  s3Exception.InnerException);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static string ReadObjectData()
        {
            string responseBody = "";

            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                using (GetObjectResponse response = client.GetObject(request))
                using (Stream responseStream = response.ResponseStream)
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string title = response.Metadata["x-amz-meta-title"];
                    Console.WriteLine("The object's title is {0}", title);

                    responseBody = reader.ReadToEnd();
                }
            }
            return responseBody;
        }
    }

    public class ListObjects
    {
        static string bucketName = "***bucket name***";
        static IAmazonS3 client;

        public void getList()
        {
            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            {
                Console.WriteLine("Listing objects stored in a bucket");
                ListingObjects();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void ListingObjects()
        {
            try
            {
                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    MaxKeys = 10
                };
                ListObjectsV2Response response;
                do
                {
                    response = client.ListObjectsV2(request);

                    // Process response.
                    foreach (S3Object entry in response.S3Objects)
                    {
                        Console.WriteLine("key = {0} size = {1}",
                            entry.Key, entry.Size);
                    }
                    Console.WriteLine("Next Continuation Token: {0}", response.NextContinuationToken);
                    request.ContinuationToken = response.NextContinuationToken;
                } while (response.IsTruncated == true);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                    "To sign up for service, go to http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine(
                     "Error occurred. Message:'{0}' when listing objects",
                     amazonS3Exception.Message);
                }
            }
        }
    }

    public class DeleteObject : Declaration
    {
        public void deleteFile(string bucketName, string keyName, string VersionId)
        {
            //client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
            IAmazonS3 client = new AmazonS3Client(_accessKey, _secretKey, RegionEndpoint.USWest2);

            DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = keyName
            };
            client.DeleteObject(deleteObjectRequest);
            //using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKeyID, secretAccessKeyID))
            //{
            //    client.DeleteObject(deleteObjectRequest);
            //}



            //Second Method
            deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = keyName,
                VersionId = VersionId
            };

            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            {
                client.DeleteObject(deleteObjectRequest);
                Console.WriteLine("Deleting an object");
            }
        }
    }

    public class DeleteObjectNonVersionedBucket
    {
        static string bucketName = "*** Provide a bucket name ***";
        static string keyName = "*** Provide a key name ****";
        static IAmazonS3 client;

        public static void Main(string[] args)
        {
            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            {
                DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };
                try
                {
                    client.DeleteObject(deleteObjectRequest);
                    Console.WriteLine("Deleting an object");
                }
                catch (AmazonS3Exception s3Exception)
                {
                    Console.WriteLine(s3Exception.Message,
                                      s3Exception.InnerException);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    public class DeleteObjectVersion : Declaration
    {
        static string bucketName = "*** Provide a Bucket Name ***";
        static string keyName = "*** Provide a Key Name ***";
        static IAmazonS3 client;

        public void delete()
        {
            using (client = new AmazonS3Client(_accessKey, _secretKey, Amazon.RegionEndpoint.USEast1))
            {
                try
                {
                    // Make the bucket version-enabled.
                    EnableVersioningOnBucket(bucketName);

                    // Add a sample object. 
                    string versionID = PutAnObject(keyName);

                    // Delete the object by specifying an object key and a version ID.
                    DeleteObjectRequest request = new DeleteObjectRequest
                    {
                        BucketName = bucketName,
                        Key = keyName,
                        VersionId = versionID
                    };
                    Console.WriteLine("Deleting an object");
                    client.DeleteObject(request);

                }
                catch (AmazonS3Exception s3Exception)
                {
                    Console.WriteLine(s3Exception.Message,
                                      s3Exception.InnerException);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void EnableVersioningOnBucket(string bucketName)
        {

            PutBucketVersioningRequest setBucketVersioningRequest = new PutBucketVersioningRequest
            {
                BucketName = bucketName,
                VersioningConfig = new S3BucketVersioningConfig { Status = VersionStatus.Enabled }
            };
            client.PutBucketVersioning(setBucketVersioningRequest);
        }

        static string PutAnObject(string objectKey)
        {

            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = objectKey,
                ContentBody = "This is the content body!"
            };

            PutObjectResponse response = client.PutObject(request);
            return response.VersionId;

        }
    }

    public class CreateBucket : Declaration
    {
        static string bucketName = "*** bucket name ***";

        public void createBucket()
        {
            using (var client = new AmazonS3Client(_accessKey, _secretKey, Amazon.RegionEndpoint.EUWest1))
            {

                if (!(AmazonS3Util.DoesS3BucketExist(client, bucketName)))
                {
                    CreateABucket(client);
                }
                // Retrieve bucket location.
                string bucketLocation = FindBucketLocation(client);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static string FindBucketLocation(IAmazonS3 client)
        {
            GetBucketLocationRequest request = new GetBucketLocationRequest()
            {
                BucketName = bucketName
            };
            GetBucketLocationResponse response = client.GetBucketLocation(request);
            var bucketLocation = response.Location.ToString();
            return bucketLocation;
        }

        static void CreateABucket(IAmazonS3 client)
        {
            try
            {
                PutBucketRequest putRequest1 = new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };

                PutBucketResponse response1 = client.PutBucket(putRequest1);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                        "For service sign up go to http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine(
                        "Error occurred. Message:'{0}' when writing an object"
                        , amazonS3Exception.Message);
                }
            }
        }
    }
}