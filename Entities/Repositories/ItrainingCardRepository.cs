using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public interface ITrainingCardRepository : IRepository<TrainingCard>
    {
        void UpdateTrainingCard(int cardID, int languageID, int userID, int newLossCount);
    }
}
