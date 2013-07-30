using System.Collections.Generic;

namespace SwaggerDriver.Swagger
{
    public interface IDocumentationClient
    {
        ServiceDocumentation Service();
        IEnumerable<ApiDocumentation> Apis(string basePath, IEnumerable<ApiReference> references);
    }
}
