using System.Collections.Generic;
using Spotify.Models;

namespace Spotify.Models;

public class ArtistsViewModel
{
    public List<Artist> Artists { get; init; }
    public string SearchString { get; init; }
    public ApplicationUser CurrentUser { get; init; }
}