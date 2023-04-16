using Data;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Logic
{
    public abstract class LogicAPI
    {
        public static LogicAPI CreateAPI()
        {
            return new BallManager();
        }

        public abstract ObservableCollection<Ball> getCollection();
        public abstract void CreateBall(int numberOfBalls);
        public abstract void MoveBall(Ball ball, double numberOfFrames, double duration, PointF vector);
        public abstract PointF FindNewBallPosition(Ball ball, int numberOfFrames);
        public abstract void BallsMovement();
    }
}
