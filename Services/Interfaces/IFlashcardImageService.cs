using Common.Operations;
using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Interfaces
{
    public interface IFlashcardImageService
    {
        MethodResult CanUploadNewImage(Flashcard flashcard, HttpPostedFileBase file);
        void UploadNewImage(Flashcard flashcard, HttpPostedFileBase file, string userID);
    }
}
