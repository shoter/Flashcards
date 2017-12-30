using Flashcards.Entities;
using Flashcards.Entities.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Implementation;
using Services.Interfaces;
using Services.Session;
using Services.Session.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSuite.Dummies;

namespace UnitTests.ServicesTests
{
    [TestClass]
    public class TrainingReviewServiceTests 
    {
        Mock<TrainingReviewService> mockTrainingReviewService;
        Mock<ISessionService> sessionService = new Mock<ISessionService>();
        Mock<IFlashcardUnit> unit = new Mock<IFlashcardUnit>();
        Mock<ITrainingRepository> trainingRepository = new Mock<ITrainingRepository>();
        Mock<IQuestionService> questionService = new Mock<IQuestionService>();
        TrainingReviewService trainingReviewService => mockTrainingReviewService.Object;

        public TrainingReviewServiceTests()
        {
            unit.Setup(x => x.TrainingRepository).Returns(trainingRepository.Object);

            mockTrainingReviewService = new Mock<TrainingReviewService>(unit.Object, sessionService.Object, questionService.Object);
            mockTrainingReviewService.CallBase = true;
        }


        [TestMethod]
        public void ShouldStopLastTraining_noLastTraining_test()
        {
            sessionService.SetupGet(x => x.UserInfo)
                .Returns(new UserInfo(new Flashcards.Entities.Models.UserInfo()));

            Assert.IsFalse(trainingReviewService.ShouldStopLastTraining());
        }

        [TestMethod]
        public void ShouldStopLastTraining_afterHour_test()
        {
            mockTraining();

            mockTrainingReviewService.Setup(x => x.CalculateTrainingExpirationDate())
                .Returns(DateTime.Now.AddHours(-1));

            Assert.IsTrue(trainingReviewService.ShouldStopLastTraining());
            mockTrainingReviewService.Verify(x => x.CalculateTrainingExpirationDate(), Times.Once);
        }

        [TestMethod]
        public void ShouldStopLastTraining_justBeforeHour_test()
        {
            mockTrainingReviewService.Setup(x => x.HasTrainingEnded(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);
            mockTraining();

            mockTrainingReviewService.Setup(x => x.CalculateTrainingExpirationDate())
                .Returns(DateTime.Now.AddMinutes(1));

            Assert.IsFalse(trainingReviewService.ShouldStopLastTraining());
            mockTrainingReviewService.Verify(x => x.CalculateTrainingExpirationDate(), Times.Once);
        }

        [TestMethod]
        public void StopLastTraining_assert_tests()
        {
            mockTraining();

            trainingReviewService.StopLastTraining();

            trainingRepository.Verify(x => x.Remove(It.IsAny<long>()), Times.Once);
            unit.Verify(x => x.SaveChanges(), Times.Once);

            Assert.AreEqual(null, sessionService.Object.UserInfo.TrainingInfo);
            
        }

        private void mockTraining()
        {
           sessionService.SetupGet(x => x.UserInfo)
                .Returns(new SessionUserInfoDummyCreator().Create());

         /*   sessionService.SetupGet(x => x.UserInfo)
                           .Returns(new UserInfo(new Flashcards.Entities.Models.UserInfo()
                           {
                               TrainingID = 1,
                               TrainingDateStarted = DateTime.Now,
                               TrainingCards = new List<Flashcards.Entities.Models.TrainingCardInfo>()
                           }));*/
        }
    }
}
