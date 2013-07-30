using System.Collections.Generic;

namespace SwaggerDriver.Swagger
{
    public interface ISwaggerClient
    {
        ServiceDocument Service();
        IEnumerable<ApiDocument> Apis(string basePath, IEnumerable<ApiReference> references);
    }
}
