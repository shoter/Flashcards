using Flashcards.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Implementation;
using Services.Implementation.ServicesTests;
using Services.Interfaces;
using Services.Session;
using Services.Session.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ServicesTests
{
    [TestClass]
    public class TrainingReviewServiceTests 
    {
        Mock<TrainingReviewService> mockTrainingReviewService;
        Mock<ISessionService> sessionService = new Mock<ISessionService>();
        TrainingReviewService trainingReviewService => mockTrainingReviewService.Object;

        public TrainingReviewServiceTests()
        {
            mockTrainingReviewService = new Mock<TrainingReviewService>(Mock.Of<IFlashcardUnit>(), sessionService.Object);
            mockTrainingReviewService.CallBase = true;
        }
        [TestMethod]
        public void IsAnswerCorrect_correct_test()
        {
            Assert.AreEqual(1.0,
                trainingReviewService.CalculateCorrectnessOfAnswer("test_answer", "test_answer"));
        }

        [TestMethod]
        public void IsAnswerCorrect_correct2_test()
        {
            Assert.AreEqual(1.0,
                trainingReviewService.CalculateCorrectnessOfAnswer("test_answer12345", "test_answer12345"));
        }

        [TestMethod]
        public void IsAnswerCorrect_not_correct_test()
        {
            Assert.IsTrue(trainingReviewService.CalculateCorrectnessOfAnswer("zupa", "testowa") < 1.0);
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
            sessionService.SetupGet(x => x.UserInfo)
               .Returns(new UserInfo(new Flashcards.Entities.Models.UserInfo()
               {
                   TrainingID = 1,
                   TrainingDateStarted = DateTime.Now,
                   TrainingCards = new List<Flashcards.Entities.Models.TrainingCardInfo>()
               }));

            mockTrainingReviewService.Setup(x => x.CalculateTrainingExpirationDate())
                .Returns(DateTime.Now.AddHours(-1));

            Assert.IsTrue(trainingReviewService.ShouldStopLastTraining());
            mockTrainingReviewService.Verify(x => x.CalculateTrainingExpirationDate(), Times.Once);
        }

        [TestMethod]
        public void ShouldStopLastTraining_justBeforeHour_test()
        {
            sessionService.SetupGet(x => x.UserInfo)
                .Returns(new UserInfo(new Flashcards.Entities.Models.UserInfo()
                {
                    TrainingID = 1,
                    TrainingDateStarted = DateTime.Now,
                    TrainingCards = new List<Flashcards.Entities.Models.TrainingCardInfo>()
                }));

            mockTrainingReviewService.Setup(x => x.CalculateTrainingExpirationDate())
                .Returns(DateTime.Now.AddMinutes(1));

            Assert.IsFalse(trainingReviewService.ShouldStopLastTraining());
            mockTrainingReviewService.Verify(x => x.CalculateTrainingExpirationDate(), Times.Once);
        }
    }
}
