using Flashcards.Entities;
using Flashcards.Entities.Enums.Files;
using Flashcards.Models.Common;
using Services.Code.Uploads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcards.Models.Manage
{
    public class EditFlashcardImageViewModel : ImageViewModel
    {
        public long FileID { get; set; }
        public string AuthorID { get; set; }
        public string OriginalFilename { get; set; }
        public DateTime UploadTime { get; set; }

        public EditFlashcardImageViewModel(FlashcardImage image) : base(image.File)
        {
            FileID = image.FileID;
            var file = image.File;

            AuthorID = file.UploadedByID;
            OriginalFilename = file.OriginalFilename;
            UploadTime = file.UploadTime;
        }
        
    }
}