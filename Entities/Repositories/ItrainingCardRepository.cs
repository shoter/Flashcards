using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public interface ITrainingCardRepository : IRepository<TrainingCard>
    {
        void UpdateTrainingCard(int cardID, int trainingID,  int newLossCount);
        List<Flashcard> GetTrainableFlaschards(int languageID, string userID, int? limit = 5);
        /// <summary>
        /// Can return null
        /// </summary>
        TrainingCard GetCardForTraining(string userID, int languageID);

        TrainingCard GetCardForTraining(long trainingID, int flashcardID);
    }
}
