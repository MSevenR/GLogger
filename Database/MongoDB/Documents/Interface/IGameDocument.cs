using GLogger.Database.MongoDB.Documents;

namespace GLogger.Database.MongoDB.Documents.Interface
{
     public interface IGameSummary
     {
            public string name { get; }
            public string summary { get; }
     }
}