using Common.utilities;
using Services.Session.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.Dummies
{
    public class SessionUserInfoDummyCreator : IDummyCreator<UserInfo>
    {
        private UserInfo userInfo;
        private TrainingInfoDummyCreator trainingInfoCreator = new TrainingInfoDummyCreator();
        private ReviewInfoDummyCreator reviewInfoCreator = new ReviewInfoDummyCreator();

        public SessionUserInfoDummyCreator()
        {
            userInfo = create();
        }

        private UserInfo create()
        {
            var userInfo =  new UserInfo(new Flashcards.Entities.Models.UserInfo()
            {
                UserID = RandomGenerator.GenerateString(128),
                Username = RandomGenerator.GenerateString(20)
            });

            userInfo.TrainingInfo = trainingInfoCreator.Create();
            userInfo.ReviewInfo = reviewInfoCreator.Create();

            return userInfo;
        }

        public SessionUserInfoDummyCreator SetReviewInfo(ReviewInfo reviewInfo)
        {
            userInfo.ReviewInfo = reviewInfo;
            return this;
        }

        public SessionUserInfoDummyCreator SetTrainingInfo(TrainingInfo trainingInfo)
        {
            userInfo.TrainingInfo = trainingInfo;
            return this;
        }

        public UserInfo Create()
        {
            var temp = userInfo;
            userInfo = create();
            return temp;
        }
    }
}
