using Common.EntityFramework;
using Flashcards.Entities.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Flashcards.Entities.Repositories
{
    public class InfoRepository : RepositoryBaseEntityless<FlashcardsEntities>, IInfoRepository
    {
        public InfoRepository(FlashcardsEntities context) : base(context)
        {
        }

        public UserInfo GetUserInfo(string userID)
        {

            return (from user in context.AspNetUsers.Where(user => user.Id == userID)
                    join training in context.TrainingSessions on user.Id equals training.UserID into trainings
                    from training in trainings.DefaultIfEmpty()
                    join trainingCard in context.TrainingCards on training.ID equals trainingCard.TrainingID into trainingCards
                    select new UserInfo()
                    {
                        UserID = userID,
                        Username = user.UserName,
                        TrainingID = training == null ? (long?)null : training.ID,
                        TrainingDateStarted = training == null ? (DateTime?)null : training.DateStarted,
                        TrainingCards = trainingCards
                        .Select(card => new TrainingCardInfo()
                        {
                            FlashcardID = card.FlashcardID,
                            InternalLossCount = card.InternalLossCount,
                            LanguageID = card.TrainingSession.LanguageID
                        })
                    }).FirstOrDefault();
        }
    }
}
