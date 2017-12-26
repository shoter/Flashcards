using Common.EntityFramework;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class TrainingFlashcardMemoryRepository : RepositoryBase<TrainingFlashcardMemory, FlashcardsEntities>, ITrainingFlashcardMemoryRepository
    {
        public TrainingFlashcardMemoryRepository(FlashcardsEntities context) : base(context)
        {
        }

        public List<Flashcard> GetTrainableFlaschards(int languageID, string userID, int? limit = 5)
        {
            var query = (from flashcard in context.Flashcards
                   join translation in context.FlashcardTranslations.Where(tr => tr.LanguageID == languageID) on flashcard.ID equals translation.FlashcardID into translations
                   join memory in context.UserFlashcardMemories.Where(mem => mem.LanguageID == languageID && mem.UserID == userID) on flashcard.ID equals memory.FlashcardID into memories
                   where memories.Count() == 0
                   select flashcard);

            if (limit.HasValue)
                query = query.TakeRandom(limit.Value);

            return query.ToList();
        }
    }
}
