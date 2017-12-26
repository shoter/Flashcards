using Common.EntityFramework;
using Flashcards.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class InfoRepository : RepositoryBaseEntityless<FlashcardsEntities>, IInfoRepository
    {
        public InfoRepository(FlashcardsEntities context) : base(context)
        {
        }

        public UserInfo GetUserInfo(string userID)
        {
            return (from training in context.TrainingSessions.Where(t => t.UserID == userID)
                    join trainingCard in context.TrainingCards on training.ID equals trainingCard.TrainingID into trainingCards

                    select new UserInfo()
                    {
                        UserID = userID,
                        TrainingID = training == null ? (long?)null : training.ID,
                        TrainingDateStarted = training == null ? (DateTime?)null : training.DateStarted,
                        TrainingCards = trainingCards
                        .Select(card => new TrainingCardInfo()
                        {
                            FlashcardID = card.FlashcardID,
                            InternalLossCount = card.InternalLossCount,
                            LanguageID = card.LanguageID
                        })
                    }).FirstOrDefault();
        }
    }
}
