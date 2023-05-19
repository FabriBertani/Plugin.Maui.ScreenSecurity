using Foundation;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

[Preserve(AllMembers = true)]
internal static class UIColorExtensions
{
    internal static UIColor FromHex(this UIColor color, string hexValue)
    {
        var colorString = hexValue.TrimStart('#');

        float red, green, blue, alpha;

        switch (colorString.Length)
        {
            // #RGB
            case 3:
                red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16) / 255f;
                green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16) / 255f;
                blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16) / 255f;

                return UIColor.FromRGB(red, green, blue);

            // #RGBA
            case 4:
                red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16) / 255f;
                green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16) / 255f;
                blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16) / 255f;
                alpha = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(3, 1)), 16) / 255f;

                return UIColor.FromRGBA(red, green, blue, alpha);

            // #RRGGBB
            case 6:
                red = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
                green = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
                blue = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;

                return UIColor.FromRGB(red, green, blue);

            // #RRGGBBAA
            case 8:
                red = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
                green = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
                blue = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
                alpha = Convert.ToInt32(colorString.Substring(6, 2), 16) / 255f;

                return UIColor.FromRGBA(red, green, blue, alpha);

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}