using GLogger.Api.IgdbApi.Endpoints;
using GLogger.Database.MongoDB.Documents.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static GLogger.Api.IgdbApi.Endpoints.Game;

namespace GLogger.Database.MongoDB.Documents
{
    public class GameDocument
    {
        private readonly GameJson _gameJson;

        public int gameid => _gameJson.id;
        public string Name => _gameJson.name ?? "Unknown";
        public string Summary => _gameJson.summary ?? "";
        public int[] Platforms => _gameJson.platforms ?? [];
        public int[] AgeRatings => _gameJson.age_ratings ?? [];
        public double AggregatedRating => _gameJson.aggregated_rating ?? -1;
        public int AggregatedRatingCount => _gameJson.aggregated_rating_count ?? -1;
        public int[] AlternativeNames => _gameJson.alternative_names ?? [];
        public int[] Artworks => _gameJson.artworks ?? [];
        public int[] Bundles => _gameJson.bundles ?? [];
        public Category Category => _gameJson.category ?? Category.Unknown;
        public Guid Checksum => _gameJson.checksum ?? Guid.Empty;
        public int Collection => _gameJson.collection ?? -1;
        public int[] Collections => _gameJson.collections ?? [];
        public int Cover => _gameJson.cover ?? -1;
        public long CreatedAt => _gameJson.created_at ?? -1;
        public int[] Dlcs => _gameJson.dlcs ?? [];
        public int[] ExpandedGames => _gameJson.expanded_games ?? [];
        public int[] Expansions => _gameJson.expansions ?? [];
        public int[] ExternalGames => _gameJson.external_games ?? [];
        public long FirstReleaseDate => _gameJson.first_release_date ?? -1;
        public int Follows => _gameJson.follows ?? -1;
        public int[] Forks => _gameJson.forks ?? [];
        public int Franchise => _gameJson.franchise ?? -1;
        public int[] Franchises => _gameJson.franchises ?? [];
        public int[] GameEngines => _gameJson.game_engines ?? [];
        public int[] GameLocalizations => _gameJson.game_localizations ?? [];
        public int[] GameModes => _gameJson.game_modes ?? [];
        public int[] Genres => _gameJson.genres ?? [];
        public int Hypes => _gameJson.hypes ?? -1;
        public int[] InvolvedCompanies => _gameJson.involved_companies ?? [];
        public int[] Keywords => _gameJson.keywords ?? [];
        public int[] LanguageSupports => _gameJson.language_supports ?? [];
        public int[] MultiplayerModes => _gameJson.multiplayer_modes ?? [];
        public int ParentGame => _gameJson.parent_game ?? -1;
        public int[] PlayerPerspectives => _gameJson.player_perspectives ?? [];
        public int[] Ports => _gameJson.ports ?? [];
        public double Rating => _gameJson.rating ?? -1;
        public int RatingCount => _gameJson.rating_count ?? -1;
        public int[] ReleaseDates => _gameJson.release_dates ?? [];
        public int[] Remakes => _gameJson.remakes ?? [];
        public int[] Remasters => _gameJson.remasters ?? [];
        public int[] Screenshots => _gameJson.screenshots ?? [];
        public int[] SimilarGames => _gameJson.similar_games ?? [];
        public string Slug => _gameJson.slug ?? "";
        public int[] StandaloneExpansions => _gameJson.standalone_expansions ?? [];
        public Status Status => _gameJson.status ?? Status.Unknown;
        public string Storyline => _gameJson.storyline ?? "";
        public int[] Tags => _gameJson.tags ?? [];

        public GameDocument(GameJson gameJson)
        {
            _gameJson = gameJson;
        }
    }
}