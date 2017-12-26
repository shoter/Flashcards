using Flashcards.Entities;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITrainingReviewService
    {
        bool ShouldStopLastTraining();
        void StartTraining(string userID, int languageID);
        void StopLastTraining();

        TrainingCard GetTrainingCard(string userID, int languageID);

        bool HasTrainingEnded(string userID, int languageID);

        void AcceptAnswer(TrainingCard trainingFlashcard);
        void DeclineAnswer(TrainingCard trainingFlashcard);

        bool IsAnswerCorrect(FlashcardAnswer answer);
    }
}
