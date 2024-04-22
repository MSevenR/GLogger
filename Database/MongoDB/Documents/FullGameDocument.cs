namespace GLogger.Database.MongoDB.Documents
{
    public sealed class FullGameDocument
    {
        public List<PlatformDocument> platforms { get; set; } = new List<PlatformDocument>();
    }
}