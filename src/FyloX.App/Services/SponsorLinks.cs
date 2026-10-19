// Where "Sponsor" can lead: the sidebar button and the About page open a window with these, the way that suits the
// language first (Korean cards and easy-pay through Fairy in Korean, GitHub Sponsors otherwise).

namespace FyloX.App.Services;

/// <summary>One way to support FyloX.</summary>
/// <param name="Id">Also the suffix of its description string, <c>sponsor.note.{Id}</c>.</param>
/// <param name="Icon">Key of the logo geometry (Styles/Brands.axaml, or Icon.Heart).</param>
/// <param name="Color">Brand colour behind the white logo.</param>
public sealed record SponsorLink(string Id, string Name, string Url, string Icon, string Color);

public static class SponsorLinks
{
    public const string GitHubSponsorsUrl = "https://github.com/sponsors/VG31OP";

    private static readonly SponsorLink GitHub = new("github", "GitHub Sponsors", GitHubSponsorsUrl, "Brand.GitHubSponsors", "#1E293B");

    /// <summary>Only GitHub Sponsors is supported.</summary>
    public static IReadOnlyList<SponsorLink> For(string language) => [GitHub];
}
