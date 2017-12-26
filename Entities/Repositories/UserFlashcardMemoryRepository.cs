using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class UserFlashcardMemoryRepository : RepositoryBase<UserFlashcardMemory, FlashcardsEntities>, IUserFlashcardMemoryRepository
    {
        public UserFlashcardMemoryRepository(FlashcardsEntities context) : base(context)
        {
        }

        public void AddBasedOnTraining(TrainingFlashcardMemory training)
        {
            Add(new UserFlashcardMemory()
            {
                FlashcardID = training.FlashcardID,
                IntervalCount = 0,
                LanguageID = training.LanguageID,
                LastInterval = 0,
                ReviewDate = DateTime.Now,
                Strength = 0.5m / (training.InternalLossCount + 1m),
                UserID = training.UserID
            });
        }
    }
}
