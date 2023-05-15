using Data;
using Logic;
using System.Drawing;

namespace TestProject_PW
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void IsBallMovedTest()
        {
            LogicAPI ballManager = LogicAPI.CreateAPI();
            ballManager.CreateBall(1);

            DataAPI ball = new Ball(100, 200, 5, 25, new PointF(2, 2), 5);
            double tmp_x = ball.X;
            double tmp_y = ball.Y;

            ballManager.MoveBall(ball);

            Assert.AreNotEqual(ball.X, tmp_x);
            Assert.AreNotEqual(ball.Y, tmp_y);
        }

        [TestMethod]
        public void BallDirectionTest()
        {
            LogicAPI ballManager = LogicAPI.CreateAPI();
            ballManager.CreateBall(1);

            DataAPI ball = new Ball(100, 200, 5, 25, new PointF(2, 2), 5);
            double tmp_x = ball.X;
            double tmp_y = ball.Y;

            ballManager.MoveBall(ball);

            Assert.AreEqual(ball.X, 102);
            Assert.AreEqual(ball.Y, 202);
        }

        [TestMethod]
        public void IsBallCreatedTest()
        {
            LogicAPI ballManager = LogicAPI.CreateAPI();
            ballManager.CreateBall(1);
            Assert.AreEqual(ballManager.getCollection().Count(), 1);
        }

        [TestMethod]
        public void InsideThaPlaneTest()
        {
            LogicAPI ballManager = LogicAPI.CreateAPI();
            ballManager.CreateBall(1);

            Assert.IsTrue(ballManager.getCollection()[0].X <= 500 - ballManager.getCollection()[0].Diameter);
            Assert.IsTrue(ballManager.getCollection()[0].Y <= 500 - ballManager.getCollection()[0].Diameter);
        }


    }
}