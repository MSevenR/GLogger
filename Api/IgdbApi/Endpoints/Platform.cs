namespace GLogger.Api.IgdbApi.Endpoints
{
    public static class Platform
    {
        public static string Endpoint = "platforms";

        public record PlatformJson(
            int id,
            string? abbreviation,
            string? alternative_name,
            Category? category,
            Guid? checksum,
            DateTime? created_at,
            int? generation,
            string? name,
            int? platform_family,
            int? platform_logo,
            string? slug,
            string? summary,
            DateTime? updated_at,
            string? url,
            int[]? websites,
            int[]? versions
        ) : BaseRecord(id);

        public enum Category
        {
            None = -1,
            Console = 1,
            Arcade = 2,
            Platform = 3,
            OperatingSystem = 4,
            PortableConsole = 5,
            Computer = 6
        }
    }
}