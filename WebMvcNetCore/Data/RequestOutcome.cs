using System;
using System.Linq;

namespace WebMvcNetCore.Data
{
    public class RequestOutcome<T>
    {
        public string RedirectUrl { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
    }
}