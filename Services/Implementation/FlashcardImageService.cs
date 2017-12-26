using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Operations;
using Flashcards.Entities;
using System.Web;
using Flashcards.Entities.Enums.Files;

namespace Services.Implementation
{
    public class FlashcardImageService : IFlashcardImageService
    {
        private readonly IUploadService uploadService;
        private readonly FlashcardUnit unit;
        public FlashcardImageService(IUploadService uploadService, FlashcardUnit unit)
        {
            this.uploadService = uploadService;
            this.unit = unit;
        }

        public MethodResult CanUploadNewImage(Flashcard flashcard, HttpPostedFileBase file)
        {
            if (flashcard == null)
                return new MethodResult("Flashcard does not exist :O!");

            return uploadService.CanUpload(file, FileTypeEnum.FlashcardImage);
        }

        public void UploadNewImage(Flashcard flashcard, HttpPostedFileBase file, string userID)
        {
            var dbFile = uploadService.Upload(file, userID, FileTypeEnum.FlashcardImage);

            unit.FlashcardImageRepository.Add(new FlashcardImage()
            {
                FileID = dbFile.ID,
                FlashcardID = flashcard.ID
            });
            
            unit.FlashcardImageRepository.SaveChanges();
        }
    }
}
