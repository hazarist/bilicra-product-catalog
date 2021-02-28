using Bilicra.ProductCatalog.Common.Models.ResponseWrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.Api.Middlewares
{
    public class ResponseWrapperMiddleware : ObjectResultExecutor
    {
        public ResponseWrapperMiddleware(OutputFormatterSelector formatterSelector, IHttpResponseStreamWriterFactory writerFactory, ILoggerFactory loggerFactory, IOptions<MvcOptions> mvcOptions) : base(formatterSelector, writerFactory, loggerFactory, mvcOptions)
        {
        }

        public override Task ExecuteAsync(ActionContext context, ObjectResult result)
        {
            var response = new ResponseWrapper
            {
                Data = result.Value,
                IsSuccess = true,
                Message = "Succesfully completed"
            };

            TypeCode typeCode = Type.GetTypeCode(result.Value.GetType());
            if (typeCode == TypeCode.Object)
                result.Value = response;

            return base.ExecuteAsync(context, result);
        }

    }
}
