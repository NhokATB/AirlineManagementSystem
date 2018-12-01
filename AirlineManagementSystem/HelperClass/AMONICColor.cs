using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaColor = System.Windows.Media.Color;
using System.Drawing;

namespace AirportManagerSystem.HelperClass
{
    class AMONICColor
    {
        public static MediaColor Business = MediaColor.FromRgb(250, 200, 38);
        public static MediaColor Dual = MediaColor.FromRgb(194, 145, 46);
        public static MediaColor CheckedIn = MediaColor.FromRgb(0, 160, 187);
        public static MediaColor Chaning = MediaColor.FromRgb(13, 79, 76);
        public static MediaColor Empty = MediaColor.FromRgb(255, 250, 203);
        public static MediaColor First = MediaColor.FromRgb(237, 214, 136);
        public static MediaColor Selected = MediaColor.FromRgb(13, 79, 76);

        public static List<MediaColor> Normal = new List<MediaColor>() { MediaColor.FromRgb(6, 75, 102), MediaColor.FromRgb(25, 106, 166) };
        public static List<MediaColor> Left = new List<MediaColor>() { MediaColor.FromRgb(247, 148, 03), MediaColor.FromRgb(25, 106, 166) };
        public static List<MediaColor> Right = new List<MediaColor>() { MediaColor.FromRgb(6, 75, 102), MediaColor.FromRgb(247, 148, 03) };

        public static MediaColor MainGold = MediaColor.FromRgb(250, 200, 38);
        public static MediaColor DarkGold = MediaColor.FromRgb(194, 145, 46);
        public static MediaColor LightBlue = MediaColor.FromRgb(0, 160, 187);
        public static MediaColor DarkGreen = MediaColor.FromRgb(13, 79, 76);
        public static MediaColor MainYellow = MediaColor.FromRgb(255, 250, 203);
        public static MediaColor LightGold = MediaColor.FromRgb(237, 214, 136);
        public static MediaColor DarkPurple = MediaColor.FromRgb(6, 75, 102);
        public static MediaColor MainBlue = MediaColor.FromRgb(25, 106, 166);
        public static MediaColor MainOrange = MediaColor.FromRgb(247, 148, 03);
        public static MediaColor DarkBlue = MediaColor.FromRgb(19, 43, 79);
    }

    class MyColor
    {
        public static Color Empty = Color.FromArgb(250, 200, 38);
        public static Color Dual = Color.FromArgb(194, 145, 46);
        public static Color CheckedIn = Color.FromArgb(0, 160, 187);
        public static Color Chaning = Color.FromArgb(13, 79, 76);
        public static Color Business = Color.FromArgb(255, 250, 203);
        public static Color First = Color.FromArgb(237, 214, 136);

        public static List<Color> Normal = new List<Color>() { Color.FromArgb(6, 75, 102), Color.FromArgb(25, 106, 166) };
        public static List<Color> Left = new List<Color>() { Color.FromArgb(247, 148, 03), Color.FromArgb(25, 106, 166) };
        public static List<Color> Right = new List<Color>() { Color.FromArgb(6, 75, 102), Color.FromArgb(247, 148, 03) };
    }
}
