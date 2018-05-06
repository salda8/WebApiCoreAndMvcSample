﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcNetCore.Data
{
    public class RequestOutcome<T>
    {
        public string RedirectUrl { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
    }
} 