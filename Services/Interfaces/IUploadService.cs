using Common.Operations;
using Flashcards.Entities.Enums.Files;
using Services.Code.Uploads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Interfaces
{
    public interface IUploadService
    {
        MethodResult CanUpload(HttpPostedFileBase file, FileTypeEnum fileType);
        Flashcards.Entities.File Upload(HttpPostedFileBase file, string userID, FileTypeEnum fileType);
        void RemoveUploadFile(Flashcards.Entities.File file);
        string GetUniqueFilePath(UploadLocation location, string extension);
    }
}
