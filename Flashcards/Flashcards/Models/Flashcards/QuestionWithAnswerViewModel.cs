using Common.Extensions;
using Flashcards.Entities;
using Flashcards.Entities.Enums.Files;
using Flashcards.Models.Common;
using Services.Code.Uploads;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcards.Models.Flashcards
{
    public class QuestionWithAnswerViewModel
    {
        public ImageViewModel Image { get; set; }
        public int FlashcardID { get; set; }

        public QuestionWithAnswerViewModel(Flashcard flashcard, int languageID)
        {
            FlashcardID = flashcard.ID;
            var image = flashcard.FlashcardImages.TakeRandom(1).FirstOrDefault();

            if (image != null)
                Image = new ImageViewModel(image.File);
        }

        public QuestionWithAnswerViewModel() { }
    }
}