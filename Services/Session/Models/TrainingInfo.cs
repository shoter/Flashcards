using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Session.Models
{
    public class TrainingInfo
    {
        public long TrainingID { get; set; }
        public DateTime DateStarted { get; set; }
        public List<TrainingCardInfo> Cards { get; set; }

        public TrainingInfo(Flashcards.Entities.Models.UserInfo userInfo)
        {
            TrainingID = userInfo.TrainingID.Value;
            DateStarted = userInfo.TrainingDateStarted.Value;
            Cards = userInfo.TrainingCards
                .Select(card => new TrainingCardInfo(card))
                .ToList();

        }


    }
}
