using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AirportManagerSystem.HelperClass
{
    class AMONICColor
    {
        public static Color Empty = Color.FromRgb(250, 200, 38);
        public static Color Dual = Color.FromRgb(194, 145, 46);
        public static Color CheckedIn = Color.FromRgb(0, 160, 187);
        public static Color Chaning = Color.FromRgb(13, 79, 76);
        public static Color Business = Color.FromRgb(255, 250, 203);
        public static Color First = Color.FromRgb(237, 214, 136);

        public static List<Color> Normal = new List<Color>() { Color.FromRgb(6, 75, 102), Color.FromRgb(25, 106, 166) };
        public static List<Color> Left = new List<Color>() { Color.FromRgb(247, 148, 03), Color.FromRgb(25, 106, 166) };
        public static List<Color> Right = new List<Color>() { Color.FromRgb(6, 75, 102), Color.FromRgb(247, 148, 03) };

        public static Color MainGold = Color.FromRgb(250, 200, 38);
        public static Color DarkGold = Color.FromRgb(194, 145, 46);
        public static Color LightBlue = Color.FromRgb(0, 160, 187);
        public static Color DarkGreen = Color.FromRgb(13, 79, 76);
        public static Color MainYellow = Color.FromRgb(255, 250, 203);
        public static Color LightGold = Color.FromRgb(237, 214, 136);
        public static Color DarkPurple = Color.FromRgb(6, 75, 102);
        public static Color MainBlue = Color.FromRgb(25, 106, 166);
        public static Color MainOrange = Color.FromRgb(247, 148, 03);
        public static Color DarkBlue = Color.FromRgb(19, 43, 79);
    }
}
