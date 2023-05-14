using Data;
using Model;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Logic
{
    public class BallManager : LogicAPI
    {
        private ObservableCollection<DataAPI> _balls = new();
        public ObservableCollection<DataAPI> Balls => _balls;

        private ObservableCollection<BallModel> _ballsModel = new();
        public override ObservableCollection<BallModel> getCollection() { return _ballsModel; }

        public override void CreateBall(int numberOfBalls)
        {
            _balls.Clear();
            _ballsModel.Clear();
            Random random = new Random();
            for (int i = 0; i < numberOfBalls; i++)
            {
                int diameter = random.Next(10, 50);
                double speed = random.Next(50, 100)/diameter;
                PointF vector = new PointF(random.Next(-5, 5)*(float)speed, random.Next(-5, 5)*(float)speed);
                Ball ball = new(random.Next(500 - diameter), random.Next(500 - diameter), diameter, diameter*diameter, vector, speed);
                _balls.Add(ball);
                _ballsModel.Add(new BallModel(ball.X, ball.Y, ball.Diameter));
            }
        }
        public override void CollisionsObserver(ObservableCollection<DataAPI> balls) // czy pilka zderza sie z inna pilka
        {
            double distanceX;
            double distanceY;

            Dictionary<(int, int), bool> bouncesDict = new Dictionary<(int, int), bool>();
            // na poczatku nie mamy zadnych zarejestrowanych odbic - wrzucamy wszedzie false, zeby nam potem nie krzyczal, że Key does not exist
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    bouncesDict[(i, j)] = false;
                }
            }

            while (true) // wykrywamy zderzenia przez caly czas dzialania programu
            {
                UpdateBallsModel();

                for (int i = 0; i < balls.Count; i++)
                {
                    for (int j = i + 1; j < balls.Count; j++)
                    {
                        distanceX = balls[i].X - balls[j].X;
                        distanceY = balls[i].Y - balls[j].Y;
                        if (Math.Sqrt(distanceX * distanceX + distanceY * distanceY) <= balls[i].Diameter/2 + balls[j].Diameter/2)
                        {
                            // jezeli obsluzylismy juz odbicie dla tej pary kulek, to pomijamy Bounce
                            if (bouncesDict[(i, j)]) continue;

                            //Console.WriteLine($"COLLISION DETECTED between:\n{balls[i].Details}\nand\n\n{balls[j].Details}\n");
                            BounceBall(balls[i], balls[j]);
                            bouncesDict[(i, j)] = true; // jezeli zrobilismy Bounce, to ustawiamy flage na true, zeby wiedziec, ze to odbicie juz zostalo obsluzone
                        }
                        else bouncesDict[(i, j)] = false; // jezeli kulki sie nie stykaja to ustawiamy flage na false, zeby bylo mozna obsluzyc kolejne zderzenie dla tej pary kulek
                    }
                }
            }
        }
        public override void BounceBall(DataAPI ball1, DataAPI ball2)  // odbijanie pilek od pilek
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
            if (_balls != null)
            {
                for (int i = 0; i < _balls.Count; i++)
                {
                    var ball = _balls[i];
                    _ballsModel[i].Update(ball.X, ball.Y, ball.Diameter);
                }
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
