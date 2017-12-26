using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Models
{
    public class UserInfo
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public long? TrainingID { get; set; }
        public DateTime? TrainingDateStarted { get; set; }

        public IEnumerable<TrainingCardInfo> TrainingCards { get; set; }
    }
}
