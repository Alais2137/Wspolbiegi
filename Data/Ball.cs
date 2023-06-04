using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Data
{
    public class Ball : DataAPI, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler? PropertyChanged;
        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly int _id;
        private double _x;
        private double _y;
        public override int Id => _id;

        public override double X
        {
            get { return _x; }
            set { _x = value; RaisePropertyChanged("X"); }
        }
        public override double Y
        {
            get { return _y; }
            set { _y = value; RaisePropertyChanged("Y"); }
        }
        private object lockObject = new object();
        public override int Diameter { get; set; }
        public override double Mass { get; set; }
        public override PointF Vector { get; set; }
        public override double Speed { get; set; }


        public override void UpdateMovement(PointF vector, double speed)
        {
            // sekcja krytyczna - tylko 1 watek na raz moze wykonac te logike
            lock (lockObject)
            {
                Vector = vector;
                Speed = speed;
            }
        }
        public Ball(int id, double x, double y, int diameter, double mass, PointF vector, double speed)
        {
            this._id = id;
            this.X = x;
            this.Y = y;
            this.Diameter = diameter;
            this.Mass = mass;
            this.Vector = vector;
            this.Speed = speed;
        }
    }
}
