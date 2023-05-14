using Data;
using Model;
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

        public abstract ObservableCollection<BallModel> getCollection();
        public abstract void CreateBall(int numberOfBalls);
        public abstract void BounceBall(DataAPI ball1, DataAPI ball2);
        public abstract void CollisionsObserver(ObservableCollection<DataAPI> balls);
        public abstract void MoveBall(DataAPI ball);
        public abstract Task BallsMovement();
    }
}
