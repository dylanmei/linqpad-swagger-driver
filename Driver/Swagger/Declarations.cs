using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SwaggerDriver.Swagger
{
    [DataContract]
    public class ApiDeclaration : SwaggerDeclaration
    {
        [DataMember(Name = "apis")]
        public List<ResourceReference> Resources { get; set; }
    }

    [DataContract]
    public class ResourceReference
    {
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }

    [DataContract]
    public class ResourceDeclaration : SwaggerDeclaration
    {
        [DataMember(Name = "resourcePath")]
        public string Path { get; set; }

        [DataMember(Name = "apis")]
        public List<Resource> Resources { get; set; }
    }

    [DataContract]
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