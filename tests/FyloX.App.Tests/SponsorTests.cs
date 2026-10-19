// "Sponsor" opens a window with every way to support FyloX, the one that suits the language first: Fairy (Korean cards
// and easy-pay) in Korean, GitHub Sponsors otherwise.

using Avalonia.Headless.XUnit;
using Avalonia.Media;
using FyloX.App.Services;
using FyloX.App.Views;
using FyloX.Core.Localization;

namespace FyloX.App.Tests;

public class SponsorTests
{
    [AvaloniaTheory]
    [InlineData("ko")]
    [InlineData("en")]
    [InlineData("zh-CN")]
    public void Only_github_sponsors_is_offered(string language)
    {
        var ways = SponsorLinks.For(language);

        Assert.Single(ways);
        Assert.Equal(SponsorLinks.GitHubSponsorsUrl, ways[0].Url);
    }
}
