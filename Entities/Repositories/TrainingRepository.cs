using Common.EntityFramework;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class TrainingRepository : RepositoryBase<TrainingSession, FlashcardsEntities>, ITrainingRepository
    {
        public TrainingRepository(FlashcardsEntities context) : base(context)
        {
        }

        public void DeleteTraining(int trainingID)
        {
            var training = Single(t => t.ID == trainingID);
            Remove(training);
        }

        public void DeleteTrianing(string userID)
        {
            var training = Single(t => t.UserID == userID);
            Remove(training);
        }

        public List<Flashcard> GetTrainableFlashcards(string userID, int languageID, int? count = null)
        {
            var query =  context.Flashcards
                .Where(f => f.UserFlashcardMemories.Any(mem => mem.UserID == userID && mem.LanguageID == languageID) == false) //it is not revieved yet.
                .Where(f => f.FlashcardTranslations.Any(trans => trans.LanguageID == languageID)); // it is in given language


            if (count.HasValue)
                query = query.TakeRandom(count.Value);

            return query.ToList();
        }

        public TrainingSession GetTrainingForUser(string userID, int languageID)
        {
            return FirstOrDefault(t => t.UserID == userID && t.LanguageID == languageID);
        }

    }
}
