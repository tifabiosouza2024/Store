namespace FS.Store.WebAPI.Helpers
{
    public class CookieHelper(ILogger<CookieHelper> logger
        , IConfiguration config
        , IHttpContextAccessor httpContextAccessor)
    {

        public readonly IResponseCookies AppendCookies = httpContextAccessor.HttpContext!.Response.Cookies;
        public readonly IRequestCookieCollection? GetCookies = httpContextAccessor.HttpContext.Request.Cookies;

        private const string Token = "Token";
        private const string RefreshToken = "RefreshToken";

        public void AppendAuthToken(string token, string refreshToken)
        {
            if (logger.IsEnabled(LogLevel.Trace))
                logger.LogTrace("Adicionando token do usuario: {T} e Refresh Token {RT}", token, refreshToken);

            Append(Token, token);
            Append(RefreshToken, refreshToken);
        }

        private void Append(string key, string value)
            => AppendCookies.Append(key, value, CookieOptions);
        private void Delete(string key)
            => AppendCookies.Delete(key, CookieOptions);

        public CookieOptions CookieOptions
           => new()
           {
               // INFO: comentar este trecho para testar por IP
               Domain = config.GetSection("Domain").Get<string>(),
               Expires = DateTime.Today.AddDays(7),
               HttpOnly = true,
               Secure = true,
               SameSite = SameSiteMode.None
           };
    }
}
