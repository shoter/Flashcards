using Common.Operations;
using Flashcards.Entities.Enums.Files;
using Flashcards.Entities.Repositories;
using Services.Code.Uploads;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Implementation
{
    public class UploadService : IUploadService
    {
        private readonly IFileRepository fileRepository;
        public UploadService(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        public virtual MethodResult CanUpload(HttpPostedFileBase file, FileTypeEnum fileType)
        {
            if (file == null || file.ContentLength == 0)
            {
                return new MethodResult("File is empty or does not exist");
            }

            if (file.ContentLength >= 5_000_000)
                return new MethodResult("File is too big! Upload something smaller than 5MB");

            return MethodResult.Success;
        }

        public Flashcards.Entities.File Upload(HttpPostedFileBase file, string userID, FileTypeEnum fileType)
        {
            string filePath = saveUploadFile(file, fileType);
            return createUploadFile(file, userID, fileType, filePath);
        }

        private string saveUploadFile(HttpPostedFileBase file, FileTypeEnum fileType)
        {
            var location = UploadLocation.Get(fileType);
            var extenson = Path.GetExtension(file.FileName).Replace(".", "");
            var filePath = GetUniqueFilePath(location, extenson);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            file.SaveAs(filePath);
            return filePath;
        }

        public void RemoveUploadFile(Flashcards.Entities.File file)
        {
            var absPath = UploadLocation.GetAbsoluteFilePathForLocation(file);
            if (File.Exists(absPath))
                File.Delete(absPath);

            fileRepository.Remove(file);
            fileRepository.SaveChanges();
        }

        private Flashcards.Entities.File createUploadFile(HttpPostedFileBase file, string userID, FileTypeEnum fileType, string filePath)
        {
            var dbFile = new Flashcards.Entities.File()
            {
                Filename = Path.GetFileName(filePath),
                FileTypeID = (int)fileType,
                OriginalFilename = file.FileName,
                UploadedByID = userID,
                UploadTime = DateTime.Now
            };

            fileRepository.Add(dbFile);
            fileRepository.SaveChanges();

            return dbFile;
        }

        public string GetUniqueFilePath(UploadLocation location, string extension)
        {
            string filePath = "";
            do
            {
                filePath = location.GetAbsoluteFilePathForLocation($"{Path.GetRandomFileName()}.{extension}");
            } while (File.Exists(filePath) == true);
            return filePath;
        }

    }
}
