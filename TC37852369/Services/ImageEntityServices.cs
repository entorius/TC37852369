using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Repository;

namespace TC37852369.Services
{
    public class ImageEntityServices
    {
        ImageEntityRepository imageEntityRepository = new ImageEntityRepository();
        LastEntityIdentificationNumberServices lastEntityIdentificationNumberServices =
            new LastEntityIdentificationNumberServices();
        string eventImagesBucketName = "eventsimages";
        string companyImagesBucketName = "companypictures";

        //getAllImagesEntitiesFromDatabase
        public async Task<List<ImageEntity>> getAllImageEntities()
        {
            return await imageEntityRepository.getAllImageEntities();
        }

        //Add ImageEntity (link to image in cloud) to database 
        private async Task<ImageEntity> AddImageEntity(string fileName,string imageNumber, string entityName, string entityId)
        {
            LastIdentificationNumber lastId = await lastEntityIdentificationNumberServices.getImageEntityLastIdentificationNumber();
            await lastEntityIdentificationNumberServices.IncreaseLastIdetificationNumber("ImageEntity");
            ImageEntity imageEntity = new ImageEntity(lastId.id, fileName, entityId, entityName, long.Parse(imageNumber));
            return await imageEntityRepository.AddImageEntity(imageEntity);
        }
        public async Task<ImageEntity> AddEventImageEntity(string fileName, string imageNumber, Event eventEntity)
        {
            return await AddImageEntity(fileName, imageNumber,  "Event", eventEntity.id.ToString());
        }
        public async Task<ImageEntity> AddCompanyImageEntity(string fileName, string imageNumber)
        {
            return await AddImageEntity(fileName, imageNumber, "CompanyData", "0");
        }

        //Add Image Entity To Cloud
        public string addEventImage(string imagePath, string imageId)
        {
            return imageEntityRepository.addImage(imagePath, imageId, eventImagesBucketName);
        }
        public string addCompanyImage(string imagePath, string imageId)
        {
            return imageEntityRepository.addImage(imagePath, imageId, companyImagesBucketName);
        }


        //Delete Image Entities From Database And Cloud
        public async Task<bool> DeleteEventImageEntity(ImageEntity imageEntity)
        {
            bool deletedFromDatabase = await imageEntityRepository.DeleteImageEntityFromDatabase(imageEntity);
            return await imageEntityRepository.deleteImageFromCloud(imageEntity, eventImagesBucketName);
        }
        public async Task<bool> DeleteCompanyImageEntity(ImageEntity imageEntity)
        {
            bool deletedFromDatabase = await imageEntityRepository.DeleteImageEntityFromDatabase(imageEntity);
            return await imageEntityRepository.deleteImageFromCloud(imageEntity, companyImagesBucketName);
        }

        private async Task<List<ImageEntity>> GetEntityImageEntities(string entityName, string entityId)
        {
            return await imageEntityRepository.GetEntityImageEntities(entityName, entityId);
        }

        public async Task<List<ImageEntity>> GetEventImageEntities(Event eventEntity)
        {
            return await this.GetEntityImageEntities("Event", eventEntity.id.ToString());
        }
        public async Task<List<ImageEntity>> GetCompanyImageEntities()
        {
            return await this.GetEntityImageEntities("CompanyData", "0");
        }

        //Downloading image from cloud
        private async Task<string> downloadImage(ImageEntity image,string savingPath, string bucketName)
        {
            return await imageEntityRepository.getBucketImage(image.link, savingPath,bucketName);
        }
        public async Task<string> downloadEventImage(ImageEntity image, string savingPath)
        {
            return await downloadImage(image, savingPath, eventImagesBucketName);
        }
        public async Task<string> downloadCompanyImage(ImageEntity image, string savingPath)
        {
            return await downloadImage(image, savingPath, companyImagesBucketName);
        }

    }
}
