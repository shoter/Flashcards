using Flashcards.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Implementation;
using Services.Interfaces;
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
    public class InternalReviewServiceTests
    {
        Mock<IFlashcardUnit> unit = new Mock<IFlashcardUnit>();
        Mock<ISessionService> sessionService = new Mock<ISessionService>();

        Mock<InternalReviewService> mockInternalReviewService;
        InternalReviewService internalReviewService => mockInternalReviewService.Object;

        private ReviewInfo reviewInfo => sessionService.Object.UserInfo.ReviewInfo;

        private SessionUserInfoDummyCreator userInfoCreator = new SessionUserInfoDummyCreator();
        public InternalReviewServiceTests()
        {
            mockInternalReviewService = new Mock<InternalReviewService>(unit.Object, sessionService.Object, Mock.Of<IUserFLashcardMemoryService>(), Mock.Of<IQuestionService>());
            mockInternalReviewService.CallBase = true;
        }

        private void mockUserInfo()
        {
            sessionService.SetupGet(x => x.UserInfo)
                .Returns(userInfoCreator.Create());
        }
        [TestMethod]
        public void MoveDeclinedFlashcardInQueue_changedPlace_test()
        {
            mockUserInfo();
            var card = reviewInfo.ReviewCards[0];

            Assert.AreEqual(reviewInfo.ReviewCards[0].FlashcardID, card.FlashcardID);
            internalReviewService.MoveDeclinedFlashcardInQueue(card.FlashcardID);
            Assert.AreNotEqual(reviewInfo.ReviewCards[0].FlashcardID, card.FlashcardID);
        }

        [TestMethod]
        public void MoveDeclinedFlashcardInQueue_somewhereElse_test()
        {
            mockUserInfo();
            var card = reviewInfo.ReviewCards[0];

            internalReviewService.MoveDeclinedFlashcardInQueue(card.FlashcardID);
            Assert.AreNotEqual(reviewInfo.ReviewCards[0].FlashcardID, card.FlashcardID);
            Assert.IsTrue(reviewInfo.ReviewCards.Contains(card));
        }

        [TestMethod]
        public void GetPlacementForCardInQueue_NotOutOfBound_test()
        {
            for (int cardCount = 1; cardCount <= 30; ++cardCount)
                for (int lossCount = 1; lossCount < 5; ++lossCount)
                {
                    int place = internalReviewService.GetPlacementForCardInQueue(cardCount, lossCount);
                    Assert.IsTrue(place >= 0);
                    Assert.IsTrue(place < cardCount);
                }
        }



    }
}
