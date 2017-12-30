using Common.EntityFramework;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class TrainingCardRepository : RepositoryBase<TrainingCard, FlashcardsEntities>, ITrainingCardRepository
    {
        public TrainingCardRepository(FlashcardsEntities context) : base(context)
        {
        }

        public void UpdateTrainingCard(int cardID,int trainingID, int newLossCount)
        {
            UpdateSingleField(
                x => x.InternalLossCount,
                x =>
                {
                    x.FlashcardID = cardID;
                    x.TrainingID = trainingID;
                }, newLossCount);
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
        /// <summary>
        /// Can return null
        /// </summary>
        public TrainingCard GetCardForTraining(string userID, int languageID)
        {
            return
                 Where(card => card.TrainingSession.UserID == userID && card.TrainingSession.LanguageID == languageID)
                 .OrderBy(card => card.InternalLossCount)
                 .Take(1)
                 .FirstOrDefault();
        }

        public TrainingCard GetCardForTraining(long trainingID, int flashcardID)
        {
            return
                 Where(card => card.TrainingID == trainingID && card.FlashcardID == flashcardID)
                 .FirstOrDefault();
        }

    }
}
