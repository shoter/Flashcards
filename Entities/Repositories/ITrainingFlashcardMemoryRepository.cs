using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public interface ITrainingFlashcardMemoryRepository : IRepository<TrainingFlashcardMemory>
    {
        /// <summary>
        /// Returns random flashcard that are not in review queues
        /// </summary>
        List<Flashcard> GetTrainableFlaschards(int languageID, string userID, int? limit = 5);
    }
}
