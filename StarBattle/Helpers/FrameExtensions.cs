using System.Windows.Controls;
using System.Windows.Media;

namespace StarBattle.Helpers
{
    public static class FrameExtensions
    {
        public static void CleanNavigation(this Frame frame)
        {
            while (frame.CanGoBack)
            {
                frame.RemoveBackEntry();
            }
        }
    }

    public static class Hues
    {
        public static Color Black => Color.FromRgb(0,0,0);
        public static Color White => Color.FromRgb(255,255,255);
        public static Color LtGray => Color.FromRgb(225, 225, 225);
    }
}
