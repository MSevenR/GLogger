using Microsoft.Extensions.Options;

namespace GLogger.Database
{
    public record DatabaseConfig(string ConnectionString,
        string DatabaseName,
        string Username,
        string Password): IOptions<DatabaseConfig> 
        {
            public DatabaseConfig Value => this;
        }
}