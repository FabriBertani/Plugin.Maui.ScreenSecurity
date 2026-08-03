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
            if (string.IsNullOrWhiteSpace(value))
            {
                _hexColor = string.Empty;

                return;
            }

            if (!string.IsNullOrEmpty(Image))
                throw new InvalidOperationException("Image is already set. Clear Image before setting HexColor.");

            _hexColor = value;
            _image = string.Empty;
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
            if (string.IsNullOrWhiteSpace(value))
            {
                _image = string.Empty;

                return;
            }

            if (!string.IsNullOrEmpty(HexColor))
                throw new InvalidOperationException("HexColor is already set. Clear HexColor before setting HexColor.");

            _image = value;
            _hexColor = string.Empty;
        }
    }

    public bool PreventScreenshot { get; set; } = true;

    public bool PreventScreenRecording { get; set; } = true;
}