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

        public void AddBasedOnTraining(TrainingCard card, decimal correctness)
        {
            Add(new UserFlashcardMemory()
            {
                FlashcardID = card.FlashcardID,
                IntervalCount = 0,
                LanguageID = card.TrainingSession.LanguageID,
                LastInterval = 0,
                ReviewDate = DateTime.Now,
                Strength = (0.5m / (card.InternalLossCount + 1m)) * correctness,
                UserID = card.TrainingSession.UserID
            });
        }
    }
}
