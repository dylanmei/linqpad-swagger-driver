using System.Runtime.Serialization;

namespace SwaggerDriver.Swagger
{
    [DataContract]
    public abstract class SwaggerDocument
    {
        [DataMember(Name = "apiVersion")]
        public string ApiVersion { get; set; }

        [DataMember(Name = "swaggerVersion")]
        public string SwaggerVersion { get; set; }

        [DataMember(Name = "basePath")]
        public string BasePath { get; set; }
    }
}