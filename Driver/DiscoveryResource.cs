using System.Collections.Generic;
using SwaggerDriver.Swagger;

namespace SwaggerDriver
{
    public class DiscoveryResource
    {
        public string Name { get; set; }
        public string BasePath { get; set; }
        public IEnumerable<Api> Apis { get; set; }
    }
}