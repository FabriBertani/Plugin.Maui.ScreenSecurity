namespace Plugin.Maui.ScreenSecurity;

public class ScreenProtectionOptions
{
    private string _hexColor = string.Empty;

    private string _image = string.Empty;

    /// <summary>
    /// Hexadecimal color as <b><c>string</c></b> in the form of 
    /// <b><c>#RGB</c></b>, <b><c>#RGBA</c></b>, <b><c>#RRGGBB</c></b> or <b><c>#RRGGBBAA</c></b>.
    /// This cannot be set if the <b><c>Image</c></b> property is already set.
    /// </summary>
    public string HexColor
    {
        get => _hexColor;
        set
        {
            if (string.IsNullOrEmpty(Image) && !string.IsNullOrEmpty(value))
                _hexColor = value;
        }
    }

    /// <summary>
    /// Name with extension of the image to use.
    /// This cannot be set if the <b><c>HexColor</c></b> property is already set.
    /// </summary>
    public string Image
    {
        get => _image;
        set
        {
            if (string.IsNullOrEmpty(HexColor) && !string.IsNullOrEmpty(value))
                _image = value;
        }
    }

    public bool PreventScreenshot { get; set; } = true;

    public bool PreventScreenRecording { get; set; } = true;
}