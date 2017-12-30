using Flashcards.Entities;
using Flashcards.Models.Flashcards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcards.Models.Reviews
{
    public class ReviewQuestionViewModel : QuestionWithAnswerViewModel
    {
        public long ReviewID { get; set; }
        public ReviewQuestionViewModel()
        {
        }

        public ReviewQuestionViewModel(ReviewCard card, int languageID) : base(card.Flashcard, languageID)
        {
            ReviewID = card.ReviewID;
        }
    }
}