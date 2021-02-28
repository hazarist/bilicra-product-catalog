using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public string ContentType { get; set; } = @"application/json";

        public NotFoundException()
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
