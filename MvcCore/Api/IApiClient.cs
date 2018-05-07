using System.Collections.Generic;
using DataStructures.Dtos;
using RestSharp;

namespace MvcCore.Api
{
    public interface IApiClient
    {
        string ApiVersion { get; set; }
        string Secretkey { get; set; }

        IRestResponse DeleteDtoSync(int id, string requestResource);
        IEnumerable<ToReturn> GetAllDtoSync<ToReturn>(string requestResource) where ToReturn : Dto, new();
        ToReturn GetByIdDtoSync<ToReturn>(string requestResource, int id) where ToReturn : Dto, new();
        ToReturn PatchDtoSync<ToReturn>(int id, string requestResource)
            where ToReturn : Dto, new();

        IRestResponse PatchDtoSync(int id, string requestResource);
           
        IRestResponse PostDtoSync<T>(T dto, string requestResource, string contentType=null, string acceptType = null) where T : Dto, new();
        ToReturn PostDtoSync<ToPost, ToReturn>(ToPost dto, string requestResource, string contentType, string acceptType = null)
            where ToPost : Dto, new()
            where ToReturn : Dto, new();
        IRestResponse PutDtoSync<T>(T dto, int id, string requestResource) where T : Dto, new();
        ToReturn PutDtoSync<ToPost, ToReturn>(ToPost dto, int id, string requestResource)
            where ToPost : Dto, new()
            where ToReturn : Dto, new();
    }
}