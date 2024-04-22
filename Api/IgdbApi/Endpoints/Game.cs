namespace GLogger.Api.IgdbApi.Endpoints
{
    public class Game
    {
        public static string Endpoint = "games";

        public record GameJson
        (
            int id,
            string? name,
            string? summary,
            int[]? platforms,
            int[]? age_ratings,
            double? aggregated_rating,
            int? aggregated_rating_count,
            int[]? alternative_names,
            int[]? artworks,
            int[]? bundles,
            Category? category,
            Guid? checksum,
            int? collection,
            int[]? collections,
            int? cover,
            long? created_at,
            int[]? dlcs,
            int[]? expanded_games,
            int[]? expansions,
            int[]? external_games,
            long? first_release_date,
            int? follows,
            int[]? forks,
            int? franchise,
            int[]? franchises,
            int[]? game_engines,
            int[]? game_localizations,
            int[]? game_modes,
            int[]? genres,
            int? hypes,
            int[]? involved_companies,
            int[]? keywords,
            int[]? language_supports,
            int[]? multiplayer_modes,
            int? parent_game,
            int[]? player_perspectives,
            int[]? ports,
            double? rating,
            int? rating_count,
            int[]? release_dates,
            int[]? remakes,
            int[]? remasters,
            int[]? screenshots,
            int[]? similar_games,
            string? slug,
            int[]? standalone_expansions,
            Status? status,
            string? storyline,
            int[]? tags,
            int[]? themes,
            double? total_rating,
            int? total_rating_count,
            long? updated_at,
            string? url,
            int? version_parent,
            string? version_title,
            int[]? videos,
            int[]? websites
        ) : BaseRecord(id);

        public enum Category
        {
            Unknown = -1,
            MainGame = 0,
            DlcAddon = 1,
            Expansion = 2,
            Bundle = 3,
            StandaloneExpansion = 4,
            Mod = 5,
            Episode = 6,
            Season = 7,
            Remake = 8,
            Remaster = 9,
            ExpandedGame = 10,
            Port = 11,
            Fork = 12,
            Pack = 13,
            Update = 14
        }

        public enum Status
        {
            Unknown = -1,
            Released = 0,
            Alpha = 2,
            Beta = 3,
            EarlyAccess = 4,
            Offline = 5,
            Cancelled = 6,
            Rumored = 7,
            Delisted = 8
        }
    }
}