// Applies ThemeSettings (light/dark, accent colour, corner radius, font order, reduced motion) at runtime by
// writing resources into Application.Resources. All styles reference these with DynamicResource.

using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using FyloX.Core.Platform;
using FyloX.Core.Settings;

namespace FyloX.App.Services;

/// <summary>Runtime theming.</summary>
public sealed class ThemeService(IPlatformServices platform)
{
    /// <summary>Accent swatches offered in the Theme page (No blue/purple; technical amber, emerald, titanium).</summary>
    public static readonly IReadOnlyList<string> Swatches =
        ["#F59E0B", "#EA580C", "#10B981", "#EF4444", "#84CC16", "#0D9488", "#78716C", "#E2E8F0"];

    /// <summary>Embedded Noto fonts (see build/fetch-fonts.ps1). Missing files fall back to system fonts.</summary>
    private const string Latin = "fonts:FyloX#Noto Sans";
    private const string Korean = "fonts:FyloX#Noto Sans KR";
    private const string Chinese = "fonts:FyloX#Noto Sans SC";
    private const string Devanagari = "fonts:FyloX#Noto Sans Devanagari";
    private const string SystemFallbacks =
        "Segoe UI, Nirmala UI, Malgun Gothic, Microsoft YaHei, PingFang SC, Apple SD Gothic Neo, Kohinoor Devanagari, sans-serif";

    public void Apply(ThemeSettings theme, string language)
    {
        var app = Application.Current ?? throw new InvalidOperationException("No application.");
        var res = app.Resources;

        app.RequestedThemeVariant = theme.Mode switch
        {
            ThemeMode.Light => ThemeVariant.Light,
            ThemeMode.Dark => ThemeVariant.Dark,
            _ => ThemeVariant.Default,
        };

        // Accent colour + derived shades. Fluent controls read the System* keys.
        var accent = Color.TryParse(theme.Accent, out var parsed) ? parsed : Color.Parse("#F59E0B");
        res["AccentColor"] = accent;
        res["AccentBrush"] = new SolidColorBrush(accent);
        res["AccentSoftBrush"] = new SolidColorBrush(accent, 0.16);
        res["AccentFaintBrush"] = new SolidColorBrush(accent, 0.08);
        res["AccentStrongBrush"] = new SolidColorBrush(Shade(accent, -0.20));
        res["OnAccentBrush"] = new SolidColorBrush(Luminance(accent) > 0.4 ? Colors.Black : Colors.White);
        res["SystemAccentColor"] = accent;
        res["SystemAccentColorLight1"] = Shade(accent, 0.15);
        res["SystemAccentColorLight2"] = Shade(accent, 0.30);
        res["SystemAccentColorLight3"] = Shade(accent, 0.45);
        res["SystemAccentColorDark1"] = Shade(accent, -0.15);
        res["SystemAccentColorDark2"] = Shade(accent, -0.30);
        res["SystemAccentColorDark3"] = Shade(accent, -0.45);

        // Technical studio radius scale: clean, crisp, engineered (not bubbly).
        var r = Math.Clamp(theme.CornerRadius, 2, 14);
        res["SoftRadius"] = new CornerRadius(r);
        res["SoftRadiusSmall"] = new CornerRadius(Math.Max(2, Math.Round(r * 0.65)));
        res["SoftRadiusLarge"] = new CornerRadius(Math.Round(r * 1.35));
        res["ControlCornerRadius"] = new CornerRadius(Math.Max(2, Math.Round(r * 0.65)));
        res["OverlayCornerRadius"] = new CornerRadius(r);

        // Han characters look different in Korean and Chinese fonts: put the UI language's font first.
        var cjk = language.StartsWith("zh", StringComparison.OrdinalIgnoreCase)
            ? $"{Chinese}, {Korean}"
            : $"{Korean}, {Chinese}";
        res["AppFontFamily"] = FontFamily.Parse($"{Latin}, {Devanagari}, {cjk}, {SystemFallbacks}");

        Motion.Configure(theme.ReduceMotion, () => platform.PrefersReducedMotion);
    }

    /// <summary>Lightens (amount &gt; 0) or darkens (amount &lt; 0) a colour.</summary>
    public static Color Shade(Color c, double amount)
    {
        byte Mix(byte v) => amount >= 0
            ? (byte)Math.Round(v + (255 - v) * amount)
            : (byte)Math.Round(v * (1 + amount));
        return Color.FromArgb(c.A, Mix(c.R), Mix(c.G), Mix(c.B));
    }

    /// <summary>Relative luminance 0..1 (sRGB approximation).</summary>
    public static double Luminance(Color c) => (0.2126 * c.R + 0.7152 * c.G + 0.0722 * c.B) / 255.0;
}
