using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.IO;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MvcCore.Api
{
    /// <summary>
    /// Class RestRequest.
    /// </summary>
    /// <seealso cref="RestSharp.RestRequest" />
    public class RestRequest : RestSharp.RestRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestRequest" /> class.
        /// </summary>
        public RestRequest()
        {
            IntializeJsonSerializer();
        }

        /// <summary>
        /// Sets Method property to value of method
        /// </summary>
        /// <param name="method">Method to use for this request</param>
        public RestRequest(Method method) : base(method)
        {
            IntializeJsonSerializer();
        }

        /// <summary>
        /// Sets Resource property
        /// </summary>
        /// <param name="resource">Resource to use for this request</param>
        public RestRequest(string resource) : base(resource)
        {
            IntializeJsonSerializer();
        }

        /// <summary>
        /// Sets Resource and Method properties
        /// </summary>
        /// <param name="resource">Resource to use for this request</param>
        /// <param name="method">Method to use for this request</param>
        public RestRequest(string resource, Method method) : base(resource, method)
        {
            IntializeJsonSerializer();
        }

        /// <summary>
        /// Sets Resource property
        /// </summary>
        /// <param name="resource">Resource to use for this request</param>
        public RestRequest(Uri resource) : base(resource)
        {
            IntializeJsonSerializer();
        }

        /// <summary>
        /// Sets Resource and Method properties
        /// </summary>
        /// <param name="resource">Resource to use for this request</param>
        /// <param name="method">Method to use for this request</param>
        public RestRequest(Uri resource, Method method) : base(resource, method)
        {
            IntializeJsonSerializer();
        }

        /// <summary>
        /// Intializes the serializer.
        /// </summary>
        protected void IntializeJsonSerializer()
        {
            JsonSerializer = new NewtonsoftJsonSerializer();
        }
    }

    ///<summary>
    /// Default JSON serializer for request bodies
    /// Doesn't currently use the SerializeAs attribute, defers to Newtonsoft's attributes
    /// </summary>
    internal class NewtonsoftJsonSerializer : ISerializer
    {
        private readonly JsonSerializer serializer;

        /// <summary>
        /// Default serializer
        /// </summary>
        public NewtonsoftJsonSerializer()
        {
            ContentType = "application/json";
            serializer = new JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        /// <summary>
        /// Default serializer with overload for allowing custom Json.NET settings
        /// </summary>
        public NewtonsoftJsonSerializer(JsonSerializer serializer)
        {
            ContentType = "application/json";
            this.serializer = serializer;
        }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Content type for serialized content
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Serialize the object as JSON
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>JSON as String</returns>
        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }
    }
}