using Common.EntityFramework;
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

        public void UpdateTrainingCard(int cardID, int languageID, int trainingID, int newLossCount)
        {
            UpdateSingleField(
                x => x.InternalLossCount,
                x =>
                {
                    x.FlashcardID = cardID;
                    x.LanguageID = languageID;
                    x.TrainingID = trainingID;
                }, newLossCount);
        }
    }
}
