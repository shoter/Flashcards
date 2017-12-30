using Common.EntityFramework;
using Common.EntityFramework.SingleChanges;
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
                IntervalCount = 1,
                LanguageID = card.TrainingSession.LanguageID,
                LastInterval = 1,
                ReviewDate = DateTime.Now.AddDays(1),
                Strength = (0.5m / (card.InternalLossCount + 1m)) * correctness,
                UserID = card.TrainingSession.UserID
            });
        }

        public List<UserFlashcardMemory> GetFirstTrainableMemories(string userID, int languageID, int? count)
        {
            count = count ?? 1;


            return Where(mem => DateTime.Now >= mem.ReviewDate && mem.LanguageID == languageID && mem.UserID == userID)
                .Where(mem => mem.Flashcard.ReviewCards.Any(rc => rc.InternalReview.LanguageID == languageID && rc.InternalReview.UserID == userID) == false) //nie możmey wybrać już przerabianej karty
                .OrderBy(mem => mem.ReviewDate)
                .Take(count.Value)
                .ToList();
        }

        public UserFlashcardMemory GetMemoryBasedOnReview(long reviewID, int flashcardID, string userID)
        {
            return (from review in context.InternalReviews.Where(r => r.ID == reviewID)
                    join flashcard in context.Flashcards on flashcardID equals flashcard.ID
                    join memory in context.UserFlashcardMemories.Where(m => m.FlashcardID == flashcardID && m.UserID == userID) on review.LanguageID equals memory.LanguageID
                    select memory
                   )
                   .FirstOrDefault();
        }


    }
}
