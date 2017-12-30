using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Session.Models
{
    public class UserInfo
    {
        public string Username { get; set; }
        public string UserID { get; set; }

        public TrainingInfo TrainingInfo { get; set; }
        public ReviewInfo ReviewInfo { get; set; }

        public UserInfo(Flashcards.Entities.Models.UserInfo userInfo)
        {
            Username = userInfo.Username;
            UserID = userInfo.UserID;

            if (userInfo.TrainingID.HasValue)
                TrainingInfo = new TrainingInfo(userInfo);

            if (userInfo.ReviewID.HasValue)
                ReviewInfo = new ReviewInfo(userInfo);
        }

    }
}
