using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    public class Ball : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private double _x;
        private double _y;
        public double X
        {
            get { return _x; }
            set { _x = value; RaisePropertyChanged("X"); }
        }
        public double Y
        {
            get { return _y; }
            set { _y = value; RaisePropertyChanged("Y"); }
        }
        public double Diameter { get; private set; }
        public double DestinationPlaneX { get; set; }
        public double DestinationPlaneY { get; set; }

        public Ball(double x, double y, double diameter, double destinationPlaneX, double destinationPlaneY)
        {
            this.X = x;
            this.Y = y;
            this.Diameter = diameter;
            this.DestinationPlaneX = destinationPlaneX;
            this.DestinationPlaneY = destinationPlaneY;
        }
    }
}
