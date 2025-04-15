using MealPlannerProject.Interfaces;
using MealPlannerProject.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Data.SqlClient;

namespace MealPlannerProjectTest
{
    [TestClass]
    public class WaterServiceTests
    {
        private IDataLink _mockDataLink = null!;
        private WaterIntakeService _waterService = null!;
        private const int TestUserId = 1;

        [TestInitialize]
        public void Setup()
        {
            _mockDataLink = Substitute.For<IDataLink>();
            _waterService = new TestableWaterService(_mockDataLink);
        }

        [TestMethod]
        [System.Obsolete]
        public void GetWaterIntake_ReturnsCorrectValue()
        {
            _mockDataLink.ExecuteScalar<float>(Arg.Any<string>(), Arg.Any<SqlParameter[]>(), false)
                .Returns(1200f);

            float result = _waterService.GetWaterIntake(TestUserId);

            Assert.AreEqual(1200f, result);
        }

        [TestMethod]
        [System.Obsolete]
        public void UpdateWaterIntake_CallsDataLinkWithCorrectParameters()
        {
            _waterService.UpdateWaterIntake(TestUserId, 1500f);

            _mockDataLink.Received(1).ExecuteQuery(
                "exec update_water_intake @UserId, @NewIntake",
                Arg.Is<SqlParameter[]>(p =>
                    (int)p[0].Value == TestUserId &&
                    (float)p[1].Value == 1500f),
                false);
        }

        [TestMethod]
        [System.Obsolete]
        public void RemoveWater300_SubtractsCorrectAmount()
        {
            _mockDataLink.ExecuteScalar<float>(Arg.Any<string>(), Arg.Any<SqlParameter[]>(), false)
                .Returns(1000f);

            _waterService.RemoveWater300(TestUserId);

            _mockDataLink.Received().ExecuteQuery(
                Arg.Any<string>(),
                Arg.Is<SqlParameter[]>(p => (float)p[1].Value == 700f),
                false);
        }

        [TestMethod]
        [System.Obsolete]
        public void RemoveWater400_SubtractsCorrectAmount()
        {
            _mockDataLink.ExecuteScalar<float>(Arg.Any<string>(), Arg.Any<SqlParameter[]>(), false)
                .Returns(1000f);

            _waterService.RemoveWater400(TestUserId);

            _mockDataLink.Received().ExecuteQuery(
                Arg.Any<string>(),
                Arg.Is<SqlParameter[]>(p => (float)p[1].Value == 600f),
                false);
        }

        [TestMethod]
        [System.Obsolete]
        public void RemoveWater500_SubtractsCorrectAmount()
        {
            _mockDataLink.ExecuteScalar<float>(Arg.Any<string>(), Arg.Any<SqlParameter[]>(), false)
                .Returns(1000f);

            _waterService.RemoveWater500(TestUserId);

            _mockDataLink.Received().ExecuteQuery(
                Arg.Any<string>(),
                Arg.Is<SqlParameter[]>(p => (float)p[1].Value == 500f),
                false);
        }

        [TestMethod]
        [System.Obsolete]
        public void RemoveWater750_SubtractsCorrectAmount()
        {
            _mockDataLink.ExecuteScalar<float>(Arg.Any<string>(), Arg.Any<SqlParameter[]>(), false)
                .Returns(1000f);

            _waterService.RemoveWater750(TestUserId);

            _mockDataLink.Received().ExecuteQuery(
                Arg.Any<string>(),
                Arg.Is<SqlParameter[]>(p => (float)p[1].Value == 250f),
                false);
        }

        [TestMethod]
        [System.Obsolete]
        public void RemoveWater_NotBelowZero()
        {
            _mockDataLink.ExecuteScalar<float>(Arg.Any<string>(), Arg.Any<SqlParameter[]>(), false)
                .Returns(200f);

            _waterService.RemoveWater500(TestUserId);

            _mockDataLink.Received().ExecuteQuery(
                Arg.Any<string>(),
                Arg.Is<SqlParameter[]>(p => (float)p[1].Value == 0f),
                false);
        }

        [TestMethod]
        [System.Obsolete]
        public void AddUserIfNotExists_UserAlreadyExists_DoesNothing()
        {
            _mockDataLink.ExecuteScalar<int>(Arg.Any<string>(), Arg.Any<SqlParameter[]>(), false)
                .Returns(1);

            _waterService.AddUserIfNotExists(TestUserId);

            _mockDataLink.DidNotReceive().ExecuteQuery(
                Arg.Is<string>(s => s.Contains("INSERT INTO")),
                Arg.Any<SqlParameter[]>(),
                false);
        }

        [TestMethod]
        [System.Obsolete]
        public void AddUserIfNotExists_UserDoesNotExist_InsertsUser()
        {
            _mockDataLink.ExecuteScalar<int>(Arg.Any<string>(), Arg.Any<SqlParameter[]>(), false)
                .Returns(0);

            _waterService.AddUserIfNotExists(TestUserId);

            _mockDataLink.Received().ExecuteQuery(
                Arg.Is<string>(s => s.Contains("INSERT INTO")),
                Arg.Is<SqlParameter[]>(p => (int)p[0].Value == TestUserId),
                false);
        }

        private class TestableWaterService : WaterIntakeService
        {
            [System.Obsolete]
            public TestableWaterService(IDataLink mockDataLink)
            {
                this.dataLink = mockDataLink;
            }
        }
    }
}
