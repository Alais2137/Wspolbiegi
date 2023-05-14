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
        private double _x;
        private double _y;
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
        private bool _canMove = true;
        public override int Diameter { get; set; }
        public override double Mass { get; set; }
        public override PointF Vector { get; set; }
        public override double Speed { get; set; }
        public override void UpdateMovement(PointF vector, double speed)
        {
            _canMove = false;
            // sekcja krytyczna - tylko 1 watek na raz moze wykonac te logike
            lock (lockObject)
            {
                Vector = vector;
                Speed = speed;
            }
            _canMove = true;
        }
        public Ball(double x, double y, int diameter, double mass, PointF vector, double speed)
        {
            this.X = x;
            this.Y = y;
            this.Diameter = diameter;
            this.Mass = mass;
            this.Vector = vector;
            this.Speed = speed;
        }
    }
}
