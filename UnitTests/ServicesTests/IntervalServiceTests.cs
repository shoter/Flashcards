using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ServicesTests
{
    [TestClass]
    public class IntervalServiceTests
    {
        Mock<IntervalService> mockIntervalService = new Mock<IntervalService>();
        IntervalService intervalService => mockIntervalService.Object;

        public IntervalServiceTests()
        {
            mockIntervalService = new Mock<IntervalService>();
            mockIntervalService.CallBase = true;
        }

        [TestMethod]
        public void CalculateIntervalForLoss_alwaysSubstractDay_test()
        {
            for (int previousInterval = 2; previousInterval < 100; ++previousInterval)
                for (double strength = 0f; strength <= 1f; strength += 0.01f)
                    for (int intervalCount = 3; intervalCount < 6; ++intervalCount)
                    {
                        var newInterval = intervalService.CalculateNewIntervalForLoss(previousInterval, strength, intervalCount);
                        Assert.IsTrue(newInterval < previousInterval);
                    }
        }

        [TestMethod]
        public void CalculateIntervalForLoss_sameIntervalForFirstAndSecond_test()
        {
            for (int previousInterval = 2; previousInterval < 100; ++previousInterval)
                for (double strength = 0f; strength <= 1f; strength += 0.01f)
                {
                    var firstInterval = intervalService.CalculateNewIntervalForLoss(previousInterval, strength, 1);
                    var secondInterval = intervalService.CalculateNewIntervalForLoss(previousInterval, strength, 2);

                    Assert.AreEqual(1, firstInterval);
                    Assert.AreEqual(6, secondInterval);
                }
        }

        [TestMethod]
        public void CalculateIntervalForWin_sameIntervalForFirstAndSecond_test()
        {
            for (int previousInterval = 2; previousInterval < 100; ++previousInterval)
                for (double strength = 0f; strength <= 1f; strength += 0.01f)
                {
                    var firstInterval = intervalService.CalculateNewIntervalForWin(previousInterval, strength, 1);
                    var secondInterval = intervalService.CalculateNewIntervalForWin(previousInterval, strength, 2);

                    Assert.AreEqual(1, firstInterval);
                    Assert.AreEqual(6, secondInterval);
                }
        }


        [TestMethod]
        public void CalculateIntervalForWin_alwaysAddDay_test()
        {
            for (int previousInterval = 1; previousInterval < 100; ++previousInterval)
                for (double strength = 0f; strength <= 1f; strength += 0.01f)
                    for (int intervalCount = 3; intervalCount < 6; ++intervalCount)
                    {
                        var newInterval = intervalService.CalculateNewIntervalForWin(previousInterval, strength, intervalCount);
                        Assert.IsTrue(newInterval > previousInterval);
                    }
        }
    }
}
