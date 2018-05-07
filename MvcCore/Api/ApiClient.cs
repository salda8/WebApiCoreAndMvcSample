using DataStructures.Dtos;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace MvcCore.Api
{
    /// <summary>
    /// Class ApiClient.
    /// </summary>
    /// <seealso cref="RestApiBase"/>
    /// <seealso cref="IApiClient"/>
    public class ApiClient : RestApiBase, IApiClient
    {
        /// <summary>
        /// The json content type
        /// </summary>
        private const string JsonContentType = "application/json";

        /// <summary>
        /// The default accept header value
        /// </summary>
        private readonly string defaultAcceptHeaderValue =
            "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="configuration">The configuration.</param>
        public ApiClient(ILogger<ApiClient> log, ApiConfiguration configuration) : base(new RestClient(configuration.Endpoint),
            log)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            //Client.Proxy = new WebProxy();
            Secretkey = configuration.SecretKey;
            ApiVersion = configuration.ApiVersion;
        }

        public string ApiVersion { get; set; }
        public string Secretkey { get; set; }

        /// <summary>
        /// Posts the dto synchronize.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="requestResource">The request resource.</param>
        /// <returns>ToReturn.</returns>
        public IRestResponse DeleteDtoSync(int id, string requestResource)

        {
            IRestRequest request = CreateJsonRequest($"{requestResource}/{id}", Method.DELETE);
            Client.ClearHandlers();

            return Execute(request);
        }

        /// <summary>
        /// Gets all dto synchronize.
        /// </summary>
        /// <typeparam name="ToReturn">The type of to return.</typeparam>
        /// <param name="requestResource">The request resource.</param>
        /// <returns>ToReturn.</returns>
        public IEnumerable<ToReturn> GetAllDtoSync<ToReturn>(string requestResource) where ToReturn : Dto, new()
        {
            IRestRequest request = CreateJsonRequest(requestResource, Method.GET);
            Client.ClearHandlers();

            return ExecuteGetAll<ToReturn>(request);
        }

        /// <summary>
        /// Gets the by identifier dto synchronize.
        /// </summary>
        /// <typeparam name="ToReturn">The type of to return.</typeparam>
        /// <param name="requestResource">The request resource.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>ToReturn.</returns>
        public ToReturn GetByIdDtoSync<ToReturn>(string requestResource, int id) where ToReturn : Dto, new()
        {
            IRestRequest request = CreateJsonRequest($"{requestResource}/{id}", Method.GET);
            Client.ClearHandlers();

            return Execute<ToReturn>(request);
        }

        public ToReturn PatchDtoSync<ToReturn>(int id, string requestResource) where ToReturn : Dto, new()
        {
            IRestRequest request = CreateJsonRequest($"{requestResource}/{id}", Method.PATCH);
            Client.ClearHandlers();
            return Execute<ToReturn>(request);
        }

        public IRestResponse PatchDtoSync(int id, string requestResource)
        {
            IRestRequest request = CreateJsonRequest($"{requestResource}/{id}", Method.PATCH);
            Client.ClearHandlers();
            return Execute(request);
        }

        /// <summary>
        /// Posts the dto synchronize.
        /// </summary>
        /// <typeparam name="ToPost">The type of to post.</typeparam>
        /// <typeparam name="ToReturn">The type of to return.</typeparam>
        /// <param name="dto">The dto.</param>
        /// <param name="requestResource">The request resource.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="acceptType">Type of the accept.</param>
        /// <returns>ToReturn.</returns>
        public ToReturn PatchDtoSync<ToPost, ToReturn>(ToPost dto, string requestResource
                ) where ToPost : Dto, new() where ToReturn : Dto, new()
        {
            IRestRequest request = CreateJsonRequest(requestResource, Method.PATCH);
            Client.ClearHandlers();
            request.AddJsonBody(dto);
            return Execute<ToReturn>(request);
        }

        /// <summary>
        /// Posts the dto synchronize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dto">The dto.</param>
        /// <param name="requestResource">The request resource.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="acceptType">Type of the accept.</param>
        /// <returns>HttpStatusCode.</returns>
        public IRestResponse PostDtoSync<T>(T dto, string requestResource, string contentType, string acceptType = null)
            where T : Dto, new()
        {
            IRestRequest request = CreateJsonRequest(requestResource, Method.POST);
            Client.ClearHandlers();

            request.AddBody(dto);
            //request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            request.AddHeader("Accept", $"{acceptType ?? defaultAcceptHeaderValue}");
            request.AddHeader("ContentType", contentType);
            request.Method = Method.POST;

            return Execute(request);
        }

        /// <summary>
        /// Posts the dto synchronize.
        /// </summary>
        /// <typeparam name="ToPost">The type of to post.</typeparam>
        /// <typeparam name="ToReturn">The type of to return.</typeparam>
        /// <param name="dto">The dto.</param>
        /// <param name="requestResource">The request resource.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="acceptType">Type of the accept.</param>
        /// <returns>ToReturn.</returns>
        public ToReturn PostDtoSync<ToPost, ToReturn>(ToPost dto, string requestResource, string contentType,
            string acceptType = null) where ToPost : Dto, new() where ToReturn : Dto, new()
        {
            IRestRequest request = CreateJsonRequest(requestResource, Method.POST);
            Client.ClearHandlers();

            request.AddJsonBody(dto);
            request.Method = Method.POST;
            return Execute<ToReturn>(request);
        }

        /// <summary>
        /// Posts the dto synchronize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dto">The dto.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="requestResource">The request resource.</param>
        /// <returns>HttpStatusCode.</returns>
        public IRestResponse PutDtoSync<T>(T dto, int id, string requestResource)
            where T : Dto, new()
        {
            IRestRequest request = CreateJsonRequest($"{requestResource}/{id}", Method.PUT);
            Client.ClearHandlers();

            request.AddJsonBody(dto);
            //request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            //request.AddHeader("Accept", $"{acceptType ?? defaultAcceptHeaderValue}");
            //request.AddHeader("ContentType", contentType);
            request.Method = Method.POST;

            return Execute(request);
        }

        /// <summary>
        /// Posts the dto synchronize.
        /// </summary>
        /// <typeparam name="ToPost">The type of to post.</typeparam>
        /// <typeparam name="ToReturn">The type of to return.</typeparam>
        /// <param name="dto">The dto.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="requestResource">The request resource.</param>
        /// <returns>ToReturn.</returns>
        public ToReturn PutDtoSync<ToPost, ToReturn>(ToPost dto, int id, string requestResource
            ) where ToPost : Dto, new() where ToReturn : Dto, new()
        {
            IRestRequest request = CreateJsonRequest($"{requestResource}/{id}", Method.PUT);
            Client.ClearHandlers();
            request.AddJsonBody(dto);
            return Execute<ToReturn>(request);
        }

        /// <summary>
        /// Creates the json request.
        /// </summary>
        /// <param name="requestResource">The request resource.</param>
        /// <param name="method">The method.</param>
        /// <returns>IRestRequest.</returns>
        private IRestRequest CreateJsonRequest(string requestResource, Method method)
        {
            return new RestRequest(requestResource)
            {
                RequestFormat = DataFormat.Json,
                Method = method,
                JsonSerializer = { ContentType = JsonContentType },
            }.AddQueryParameter("api-version", ApiVersion)
             .AddHeader("x-api-key", Secretkey);
        }
    }
}