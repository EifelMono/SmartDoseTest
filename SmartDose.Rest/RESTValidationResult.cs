using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SmartDose.REST.Base.Validation
{
    public class RESTValidationResult
    {
        public RESTValidationResult(bool ok, string infos)
        {
            Ok = ok;
            Infos = infos;
            StatusCode = ok ? HttpStatusCode.OK : HttpStatusCode.PreconditionFailed;
        }
        public bool Ok { get; set; }
        public string Infos { get; set; } = "";

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotImplemented;
    }
}