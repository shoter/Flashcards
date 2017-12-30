using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSuite.Dummies;

namespace UnitTests.DummyTests
{
    [TestClass]
    public class ReviewInfoCreatorTests
    {
        [TestMethod]
        public void ReivewInfoCreator_uniqueCardsIDs_test()
        {
            var info = new ReviewInfoDummyCreator().Create();

            int cardCount = info.ReviewCards.Count;
            int uniqueCardCount = info.ReviewCards
                .Select(c => c.FlashcardID).Distinct().Count();

            Assert.AreEqual(cardCount, uniqueCardCount);
        }
    }
}
