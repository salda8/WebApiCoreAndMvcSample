namespace MvcCore.Api
{
    /// <summary>
    /// Class ApiConfiguration.
    /// </summary>
    public class ApiConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConfiguration" /> class.
        /// </summary>
        /// <param name="secretKey">The secret key.</param>
        /// <param name="apiVersion">The API version.</param>
        /// <param name="endpoint">The endpoint.</param>
        public ApiConfiguration(string secretKey, string apiVersion, string endpoint)
        {
            SecretKey = secretKey;
            ApiVersion = apiVersion;
            Endpoint = endpoint;
        }

        public ApiConfiguration()
        {
        }

        public string ApiVersion { get; set; }
        public string Endpoint { get; set; }

        public string SecretKey { get; set; }
    }
}