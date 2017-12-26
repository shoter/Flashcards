using Flashcards.Models.Flashcards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flashcards.Entities;

namespace Flashcards.Models.Trainings
{
    public class TrainingQuestion : QuestionWithAnswerViewModel
    {
        public long TrainingID { get; set; }

        public TrainingQuestion()
        {
        }

        public TrainingQuestion(TrainingCard card, int languageID) : base(card.Flashcard, languageID)
        {
            TrainingID = card.TrainingID;
        }
    }
}