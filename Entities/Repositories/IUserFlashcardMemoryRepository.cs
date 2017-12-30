using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public interface IUserFlashcardMemoryRepository : IRepository<UserFlashcardMemory>
    {
        void AddBasedOnTraining(TrainingCard training, decimal correctness);
        UserFlashcardMemory GetMemoryBasedOnReview(long reviewID, int flashcardID, string userID);
        List<UserFlashcardMemory> GetFirstTrainableMemories(string userID, int languageID, int? count);
    }
}
