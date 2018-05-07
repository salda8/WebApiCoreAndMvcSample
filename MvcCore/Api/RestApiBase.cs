using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using DataStructures.Dtos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace MvcCore.Api
{
    /// <summary>
    /// Class RestApiBase.
    /// </summary>
    public abstract class RestApiBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestApiBase"/> class.
        /// </summary>
        /// <param name="restClient">The rest client.</param>
        /// <param name="logger">The logger.</param>
        protected RestApiBase(IRestClient restClient, ILogger<ApiClient> logger)
        {
            Client = restClient;
            Logger = logger;
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>The client.</value>
        protected IRestClient Client { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogger Logger { get; }

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IRestResponse.</returns>
        /// <exception cref="ApiException">alse)</exception>
        /// <exception cref="Exception"></exception>
        protected virtual IRestResponse Execute(IRestRequest request)
        {
            IRestResponse response = null;
            var stopWatch = new Stopwatch();

            try
            {
                stopWatch.Start();
                response = Client.Execute(request);
                stopWatch.Stop();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new ApiException($"Bad Request. Response Message: {response.Content}");
                    case HttpStatusCode.InternalServerError:
                        throw new ApiException("Generic exception.");
                    case HttpStatusCode.NotFound:
                        throw new ApiException("Endpoint not found");
                    case HttpStatusCode.Forbidden:
                        throw new ApiException("Forbidden.");
                }

                // CUSTOM CODE: Do more stuff here if you need to...

                return response;
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw new Exception($"Unknown Api exception. {ex}");
            }
            finally
            {
                LogRequest(request, response, stopWatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">The request.</param>
        /// <returns>T.</returns>
        protected virtual T Execute<T>(IRestRequest request) where T : Dto, new()
        {
            var stopWatch = new Stopwatch();
           
            IRestResponse response = null;
            try
            {
                stopWatch.Start();
                response = Client.Execute(request);
                stopWatch.Stop();
                
                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new ApiException($"Bad Request. Response Message: {response.Content}");
                    case HttpStatusCode.InternalServerError:
                        throw new ApiException("Generic exception.");
                    case HttpStatusCode.NotFound:
                        throw new ApiException("Endpoint not found");
                    case HttpStatusCode.Forbidden:
                        throw new ApiException("Forbidden.");
                }

                var dto = JsonConvert.DeserializeObject<T>(response.Content);
                return dto;
            }
            catch (ApiException e)
            {
                Logger.LogError(e, e.Message);
                throw;
            }
            finally
            {
                LogRequest(request, response, stopWatch.ElapsedMilliseconds);
            }

        }

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">The request.</param>
        /// <returns>T.</returns>
        protected virtual IEnumerable<T> ExecuteGetAll<T>(IRestRequest request) where T : Dto, new()
        {
            var stopWatch = new Stopwatch();

            IRestResponse response = null;
            try
            {
                stopWatch.Start();
                response = Client.Execute(request);
                stopWatch.Stop();

                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new ApiException($"Bad Request. Response Message: {response.Content}");
                    case HttpStatusCode.InternalServerError:
                        throw new ApiException("Generic exception.");
                    case HttpStatusCode.NotFound:
                        throw new ApiException("Endpoint not found");
                    case HttpStatusCode.Forbidden:
                        throw new ApiException("Forbidden.");
                }

                var dto = JsonConvert.DeserializeObject<IEnumerable<T>>(response.Content);
                return dto;
            }
            catch (ApiException e)
            {
                Logger.LogError(e, e.Message);
                throw;
            }
            finally
            {
                LogRequest(request, response, stopWatch.ElapsedMilliseconds);
            }

        }

        /// <summary>
        /// Logs the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <param name="durationMs">The duration ms.</param>
        private void LogRequest(IRestRequest request, IRestResponse response, long durationMs)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),

                method = request.Method.ToString(),

                uri = Client.BuildUri(request)
            };

            var responseToLog = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                headers = response.Headers,
                responseUri = response.ResponseUri,
                errorMessage = response.ErrorMessage,

            };

            Logger.LogInformation(
                $"Request completed in {durationMs} ms, Request: {JsonConvert.SerializeObject(requestToLog)}, Response: {JsonConvert.SerializeObject(responseToLog)}. API version:{Assembly.GetExecutingAssembly().GetName().Version}");
        }
    }

    public class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
          
        }
    }
}