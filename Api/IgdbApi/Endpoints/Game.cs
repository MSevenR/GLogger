namespace GLogger.Api.IgdbApi.Endpoints
{
    public class Game
    {
        public static string Endpoint = "games";
    }

    public record GameJson(int id,
        string name,
        string? summary,
        int[]? platforms);
}