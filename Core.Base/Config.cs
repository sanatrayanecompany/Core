namespace Core.Base
{
    internal class Config
    {
        public string ConnectionString { get; set; }

        public TokenConfig Token { get; set; }

        public OriginConfig Origin { get; set; }

        public bool ExceptionLog { get; set; }

        public bool CamelCaseJsonResponse { get; set; }
    }

    internal class TokenConfig
    {
        public bool Enable { get; set; }
        public int Minute { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
    }

    internal class OriginConfig
    {
        public string AccessControlAllowOrigin { get; set; }
        public string AccessControlAllowHeaders { get; set; }
        public string AccessControlAllowMethods { get; set; }
        public string AccessControlAllowCredentials { get; set; }
    }
}