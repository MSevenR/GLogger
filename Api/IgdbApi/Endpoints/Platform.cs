namespace GLogger.Api.IgdbApi.Endpoints
{
    public static class Platform
    {
        public static string Endpoint = "platforms";

        public record PlatformJson(int id,
            string alternative_name,
            string name,
            string checksum);
    }
}