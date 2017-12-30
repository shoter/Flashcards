using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public interface ITrainingRepository : IRepository<TrainingSession>
    {
        void DeleteTraining(int trainingID);
        void DeleteTrianing(string userID);
        List<Flashcard> GetTrainableFlashcards(string userID, int languageID, int? count = null);
        TrainingSession GetTrainingForUser(string userID, int languageID);



    }
}
