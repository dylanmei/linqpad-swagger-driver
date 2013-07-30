using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SwaggerDriver.Swagger
{
    [DataContract]
    public class ServiceDocument : SwaggerDocument
    {
        [DataMember(Name = "apis")]
        public List<ApiReference> Apis { get; set; }
    }

    [DataContract]
    public class ApiReference
    {
        [DataMember(Name = "path")]
        public string DocPath { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}