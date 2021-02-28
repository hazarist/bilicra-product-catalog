using Newtonsoft.Json;

namespace Bilicra.ProductCatalog.Common.Models.ResponseWrapper
{
    public class ResponseWrapper
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
