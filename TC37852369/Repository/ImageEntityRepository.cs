using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;



namespace TC37852369.Repository
{
    
    public class ImageEntityRepository
    {
        public async Task<List<ImageEntity>> getAllImageEntities()
        {
            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);

            List<ImageEntity> allImages = new List<ImageEntity>();

            Query allImageQuery = db.Collection("Images");
            QuerySnapshot allImageQuerySnapshot = await allImageQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allImageQuerySnapshot.Documents)
            {
                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
                Dictionary<string, object> imageValue = documentSnapshot.ToDictionary();
                ImageEntity imageEntity = new ImageEntity(
                    long.Parse(imageValue["Id"].ToString()),
                    imageValue["Link"].ToString(),
                    imageValue["EntityId"].ToString(),
                    imageValue["EntityName"].ToString(),
                    long.Parse(imageValue["ImageNumber"].ToString())
 
                    );

                allImages.Add(imageEntity);
            }
            return allImages;
        }
        public async Task<ImageEntity> AddImageEntity(ImageEntity imageEntity)
        {
            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id",                         imageEntity.id                      },
                { "Link",                       imageEntity.link                    },
                { "EntityId",                   imageEntity.entityId                },
                { "EntityName",                 imageEntity.entityName              },
                { "ImageNumber",                imageEntity.imageNumber             }
            };
            DocumentReference docRef = db.Collection("Images").Document(imageEntity.id.ToString());
            await docRef.SetAsync(user);
            return imageEntity;
        }
        public async Task<bool> DeleteImageEntityFromDatabase(ImageEntity imageEntity)
        {
            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);
            DocumentReference docRef = db.Collection("Images").Document(imageEntity.id.ToString());
            await docRef.DeleteAsync();
            return true;
        }
        public async Task<List<ImageEntity>> GetEntityImageEntities(string entityName,string entityId)
        {
            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);

            List<ImageEntity> entityImages = new List<ImageEntity>();

            Query entityImagesQuery = db.Collection("Images").WhereEqualTo("EntityName", entityName).WhereEqualTo("EntityId",entityId);
            QuerySnapshot entityImagesQuerySnapshot = await entityImagesQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in entityImagesQuerySnapshot.Documents)
            {
                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
                Dictionary<string, object> imageValue = documentSnapshot.ToDictionary();
                ImageEntity imageEntity = new ImageEntity(
                    long.Parse(imageValue["Id"].ToString()),
                    imageValue["Link"].ToString(),
                    imageValue["EntityId"].ToString(),
                    imageValue["EntityName"].ToString(),
                    long.Parse(imageValue["ImageNumber"].ToString())

                    );

                entityImages.Add(imageEntity);
            }
            return entityImages;
        }

        public async Task<string> getBucketImage(string eventImageName,string savingPath, string bucketName)
        {
            string imagePath = savingPath + eventImageName;
            try
            {
                if (eventImageName.Length != 0 && savingPath.Length != 0)
                {
                    string googleCloudEnvVar = SetEnvironmentVariable.getGoogleCloudEnvironmentVariable();
                    GoogleCredential credential = GoogleCredential.FromFile(googleCloudEnvVar);
                    var storage = StorageClient.Create(credential);

                    using (var outputFile = File.OpenWrite(savingPath+ @"\" + eventImageName))
                    {
                        
                        Console.WriteLine(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
                        foreach(DictionaryEntry env in Environment.GetEnvironmentVariables())
                        {
                            string name = (string)env.Key;
                            string value = (string)env.Value;
                            Console.WriteLine("{0}={1}", name, value);
                        }
                        await storage.DownloadObjectAsync(bucketName, eventImageName, outputFile);
                    }
                    Console.WriteLine($"downloaded {eventImageName} to {savingPath}.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return imagePath;
        }
        public async Task<bool> deleteEventImageFromCloud(ImageEntity imageEntity)
        {
            bool imageDeleted = false;
            try
            {
                if (imageEntity != null)
                {
                    string bucketName = "eventsimages1";

                    string googleCloudEnvVar = SetEnvironmentVariable.getGoogleCloudEnvironmentVariable();
                    GoogleCredential credential = GoogleCredential.FromFile(googleCloudEnvVar);
                    var storageClient = StorageClient.Create(credential);

                    await storageClient.DeleteObjectAsync(bucketName, imageEntity.link);
                    Console.WriteLine("uploaded the file successfully");
                    imageDeleted = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return imageDeleted;
        }
       
        public async Task<bool> deleteImageFromCloud(ImageEntity imageEntity,string bucketName)
        {
            bool imageDeleted = false;
            try
            {
                if (imageEntity != null)
                {

                    string googleCloudEnvVar = SetEnvironmentVariable.getGoogleCloudEnvironmentVariable();
                    GoogleCredential credential = GoogleCredential.FromFile(googleCloudEnvVar);
                    var storageClient = StorageClient.Create(credential);

                    await storageClient.DeleteObjectAsync(bucketName, imageEntity.link);
                    Console.WriteLine("uploaded the file successfully");
                    imageDeleted = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return imageDeleted;
        }
        /*public string addEventImage(string imagePath, string imageId)
        {
            SetEnvironmentVariable.setGoogleCloudEnvironmentVariable();
            string eventImagesLinks = "";
            try
            {
                if (imagePath.Length != 0)
                {
                    string bucketName = "eventsimages";
                    string sharedkeyFilePath = SetEnvironmentVariable.getGoogleCloudEnvironmentVariable();
                    GoogleCredential credential = null;
                    using (var jsonStream = new FileStream(sharedkeyFilePath, FileMode.Open,
                        FileAccess.Read, FileShare.Read))
                    {
                        credential = GoogleCredential.FromStream(jsonStream);
                    }
                    var storageClient = StorageClient.Create(credential);
                    string[] spliters = { @"\" };

                    string filetoUpload = imagePath;
                    string[] splitedString = imagePath.Split(spliters, StringSplitOptions.RemoveEmptyEntries);
                    string fileName = splitedString[splitedString.Length - 1];
                    eventImagesLinks = fileName;


                    string[] spliters1 = { "."};

                    string[] splitedFileName = fileName.Split(spliters1, StringSplitOptions.RemoveEmptyEntries);
                    string newFileName = "";
                    for(int i=0;i<splitedFileName.Length;i++)
                    {
                        if (i != 0)
                        {
                            newFileName += ".";
                        }
                        newFileName += splitedFileName[i];
                        if (i == splitedFileName.Length - 2)
                        {
                            newFileName = newFileName + imageId;
                        }
                    }
                    eventImagesLinks = newFileName;
                    //check if object with name like this exists
                    try
                    {
                        Google.Apis.Storage.v1.Data.Object GoogleFirestoreObject = storageClient.GetObject(bucketName, fileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("GoogleFirestoreObject does not exist");
                    }
                    using (var fileStream = new FileStream(filetoUpload, FileMode.Open,
                        FileAccess.Read, FileShare.Read))
                    {
                        storageClient.UploadObject(bucketName, newFileName, "text/plain", fileStream);

                    }
                    Console.WriteLine("uploaded the file successfully");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            return eventImagesLinks;
        }*/
        public string addImage(string imagePath, string imageId,string bucketName)
        {
            SetEnvironmentVariable.setGoogleCloudEnvironmentVariable();
            string eventImageLink = "";
            try
            {
                if (imagePath.Length != 0)
                {
                    string sharedkeyFilePath = SetEnvironmentVariable.getGoogleCloudEnvironmentVariable();
                    GoogleCredential credential = null;
                    using (var jsonStream = new FileStream(sharedkeyFilePath, FileMode.Open,
                        FileAccess.Read, FileShare.Read))
                    {
                        credential = GoogleCredential.FromStream(jsonStream);
                    }
                    var storageClient = StorageClient.Create(credential);
                    string[] spliters = { @"\" };

                    string filetoUpload = imagePath;
                    string[] splitedString = imagePath.Split(spliters, StringSplitOptions.RemoveEmptyEntries);
                    string fileName = splitedString[splitedString.Length - 1];


                    string[] spliters1 = { "." };

                    string[] splitedFileName = fileName.Split(spliters1, StringSplitOptions.RemoveEmptyEntries);
                    string newFileName = "";
                    for (int i = 0; i < splitedFileName.Length; i++)
                    {
                        if (i != 0)
                        {
                            newFileName += ".";
                        }
                        newFileName += splitedFileName[i];
                        if (i == splitedFileName.Length - 2)
                        {
                            newFileName = newFileName + imageId;
                        }
                    }
                    eventImageLink = newFileName;
                    //check if object with name like this exists
                    try
                    {
                        Google.Apis.Storage.v1.Data.Object GoogleFirestoreObject = storageClient.GetObject(bucketName, fileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("GoogleFirestoreObject does not exist");
                    }
                    using (var fileStream = new FileStream(filetoUpload, FileMode.Open,
                        FileAccess.Read, FileShare.Read))
                    {
                        storageClient.UploadObject(bucketName, newFileName, "text/plain", fileStream);

                    }
                    Console.WriteLine("uploaded the file successfully");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            return eventImageLink;
        }

    }
}
