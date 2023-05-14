using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class BallModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double X { get; private set; }
        public double Y { get; private set; }
        public int Diameter { get; private set; }

        public BallModel(double x, double y, int d)
        {
            X = x;
            Y = y;
            Diameter = d;
        }

        public void Update(double x, double y, int d)
        {
            X = x;
            RaisePropertyChanged(nameof(X));
            Y = y;
            RaisePropertyChanged(nameof(Y));
            Diameter = d;
            RaisePropertyChanged(nameof(Diameter));
        }
    }
}
