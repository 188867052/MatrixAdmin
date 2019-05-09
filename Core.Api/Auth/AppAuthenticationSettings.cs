namespace Core.Api.Auth
{
    /// <summary>
    /// JWT授权的配置项.
    /// </summary>
    public class AppAuthenticationSettings
    {
        /// <summary>
        /// 应用ID.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 应用密钥(真实项目中可能区分应用,不同的应用对应惟一的密钥).
        /// </summary>
        public string Secret { get; set; }
    }
}