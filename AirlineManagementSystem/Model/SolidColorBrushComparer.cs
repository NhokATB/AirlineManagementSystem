using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AirportManagerSystem.Model
{
    public class SolidColorBrushComparer : IEqualityComparer<SolidColorBrush>
    {
        public bool Equals(SolidColorBrush x, SolidColorBrush y)
        {
            return x.Color == y.Color &&
                x.Opacity == y.Opacity;
        }

        public int GetHashCode(SolidColorBrush obj)
        {
            return new { C = obj.Color, O = obj.Opacity }.GetHashCode();
        }
    }
}
