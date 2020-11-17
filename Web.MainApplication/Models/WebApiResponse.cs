using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainApplication.Models
{
    public class WebApiResponse
    {
        public string Message { get; set; }
        public object Body { get; set; }
    }
}