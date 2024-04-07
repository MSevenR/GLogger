namespace GLogger.TwitchApi
{
    public record AccessToken(string access_token,
        int expires_in,
        string token_type)
        {
            public DateTime ExpiresAt => DateTime.Now.AddSeconds(expires_in);
        }
}