using Data;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Logic
{
    public class BallManager : LogicAPI
    {
        private ObservableCollection<Ball> _balls = new();
        public ObservableCollection<Ball> Balls { get { return _balls; } }
        public override ObservableCollection<Ball> getCollection() { return _balls; }

        public override void CreateBall(int numberOfBalls)
        {
            _balls.Clear();
            Random random = new Random();
            for (int i = 0; i < numberOfBalls; i++)
            {
                int diameter = random.Next(1, 50);
                Ball ball = new(random.Next(500 - diameter), random.Next(500 - diameter), diameter, 0, 0);
                _balls.Add(ball);
            }
        }
        public override PointF FindNewBallPosition(Ball ball, int numberOfFrames)
        {
            Random random = new Random();
            ball.DestinationPlaneX = random.Next(0, 500 - (int)ball.Diameter);
            ball.DestinationPlaneY = random.Next(0, 500 - (int)ball.Diameter);
            double length = Math.Sqrt(Math.Pow(ball.DestinationPlaneX - ball.X, 2) + Math.Pow(ball.DestinationPlaneY - ball.Y, 2)) * numberOfFrames;
            PointF vector = new PointF();
            vector.X = (float)((ball.DestinationPlaneX - ball.X) / length);
            vector.Y = (float)((ball.DestinationPlaneY - ball.Y) / length);
            return vector;
        }
        public override void MoveBall(Ball ball, double numberOfFrames, double duration, PointF vector)
        {
            ball.X += vector.X;
            ball.Y += vector.Y;
            Thread.Sleep(1);
            /*            Thread.Sleep((int)((duration / numberOfFrames) * 100));*/
        }

        public override void BallsMovement()
        {
            foreach (Ball ball in _balls)
            {
                Thread thread = new Thread(() =>
                {
                    PointF vector = FindNewBallPosition(ball, 5);
                    while (true)
                    {
                        if ((vector.X > 0 && ball.X > 500 - (int)ball.Diameter) ||
                            (vector.X < 0 && ball.X < 0) ||
                            (vector.Y > 0 && ball.Y > 500 - (int)ball.Diameter) ||
                            (vector.Y < 0 && ball.Y < 0))
                        {
                            vector = FindNewBallPosition(ball, 5);
                        }
                        MoveBall(ball, 100, 1, vector);
                    }
                });
                thread.Start();
            }
        }
    }
}
