using Data;
using Model;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Logic
{
    public class BallManager : LogicAPI
    {
        private ObservableCollection<DataAPI> _balls = new();

        private ObservableCollection<BallModel> _ballsModel = new();
        public override ObservableCollection<BallModel> getCollection() { return _ballsModel; }

        private Logger? _logger;

        public override void CreateBall(int numberOfBalls)
        {

            if (File.Exists(@"..\..\..\..\..\logs.json"))
                File.Delete(@"..\..\..\..\..\logs.json");
            _balls.Clear();
            _ballsModel.Clear();
            Random random = new();
            for (int i = 0; i < numberOfBalls; i++)
            {
                int diameter = random.Next(10, 50);
                double speed = random.Next(50, 100)/diameter;
                PointF vector = new((float)((-1 + 2 * random.NextDouble())*speed), (float)((-1 + 2 * random.NextDouble()) * speed));
                Ball ball = new(i, random.Next(500 - diameter), random.Next(500 - diameter), diameter, diameter*diameter, vector, speed);
                _balls.Add(ball);
                _ballsModel.Add(new BallModel(ball.X, ball.Y, ball.Diameter));
            }
            _logger = new Logger(_balls);
        }
        public override void CollisionsObserver(ObservableCollection<DataAPI> balls)
        {
            double distanceX;
            double distanceY;

            Dictionary<(int, int), bool> bouncesDict = new Dictionary<(int, int), bool>();

            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    bouncesDict[(i, j)] = false;
                }
            }

            while (_balls.Count != 0) 
            {
                UpdateBallsModel();

                for (int i = 0; i < balls.Count; i++)
                {
                    for (int j = i + 1; j < balls.Count; j++)
                    {
                        distanceX = (balls[i].X + balls[i].Diameter/2) - (balls[j].X + balls[j].Diameter/2);
                        distanceY = (balls[i].Y + balls[i].Diameter/2) - (balls[j].Y + balls[j].Diameter / 2);
                        if (Math.Sqrt(distanceX * distanceX + distanceY * distanceY) <= balls[i].Diameter/2 + balls[j].Diameter/2)
                        {

                            if (bouncesDict[(i, j)]) continue;

                            BounceBall(balls[i], balls[j]);
                            bouncesDict[(i, j)] = true; 
                        }
                        else bouncesDict[(i, j)] = false; 
                    }
                }
            }
        }
        public override void BounceBall(DataAPI ball1, DataAPI ball2) 
        {
            PointF tmp = ball1.Vector;
            double temp = ball1.Speed + ((2 * ball2.Mass) / (ball1.Mass + ball2.Mass));
            double temp2 = ball2.Speed + ((2 * ball1.Mass) / (ball1.Mass + ball2.Mass));
            ball1.UpdateMovement(ball2.Vector, temp);
            ball2.UpdateMovement(tmp, temp2);
        }

        public override void MoveBall(DataAPI ball)
        {
            ball.X += ball.Vector.X;
            ball.Y += ball.Vector.Y;
            Thread.Sleep(10);
        }
        private void UpdateBallsModel()
        {
            for (int i = 0; i < _balls.Count; i++)
            {
                var ball = _balls[i];
                _ballsModel[i].Update(ball.X, ball.Y, ball.Diameter);
            }
        }

        public override async Task BallsMovement()
        {
            foreach (DataAPI ball in _balls)
            {
                Task movement = new Task(() =>
                {
                    while (true)
                    {
                        if ((ball.Vector.X > 0 && ball.X > 500 - (int)ball.Diameter) || (ball.Vector.X < 0 && ball.X < 0))
                        {
                            ball.Vector = new PointF
                            {
                                X = -ball.Vector.X,
                                Y = ball.Vector.Y
                            };
                        }
                        if ((ball.Vector.Y > 0 && ball.Y > 500 - (int)ball.Diameter) || (ball.Vector.Y < 0 && ball.Y < 0))
                        {
                            ball.Vector = new PointF
                            {
                                X = ball.Vector.X,
                                Y = -ball.Vector.Y
                            };
                        }
                        MoveBall(ball);
                    }
                });
                movement.Start();
            }
            Task ballCollisions = new Task(() => CollisionsObserver(_balls));
            ballCollisions.Start();
        }
    }
}
