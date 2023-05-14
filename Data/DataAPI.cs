using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Data
{
    public abstract class DataAPI
    {
        public abstract event PropertyChangedEventHandler? PropertyChanged;
        protected abstract void RaisePropertyChanged([CallerMemberName] string propertyName = null);
        public abstract double X
        {
            get; set;
        }
        public abstract double Y
        {
            get; set;
        }
        public abstract double Speed { get; set; }
        public abstract int Diameter { get; set; }
        public abstract double Mass { get; set; }
        public abstract PointF Vector { get; set; }
        public abstract void UpdateMovement(PointF vector, double speed);
    }
}
