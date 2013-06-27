using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SwaggerDriver.Swagger
{
    [DataContract]
    public class ServiceDeclaration : SwaggerDeclaration
    {
        [DataMember]
        public List<ApiReference> Apis { get; set; }
    }

    [DataContract]
    public class ApiReference
    {
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }

    public class ApiDeclaration : SwaggerDeclaration
    {
        [DataMember(Name = "resourcePath")]
        public string Path { get; set; }

        [DataMember]
        public List<Api> Apis { get; set; }
    }

    public class SwaggerDeclaration
    {
        [DataMember(Name = "apiVersion")]
        public string ApiVersion { get; set; }

        [DataMember(Name = "swaggerVersion")]
        public string SwaggerVersion { get; set; }

        [DataMember(Name = "basePath")]
        public string BasePath { get; set; }
    }
}