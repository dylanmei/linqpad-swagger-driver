using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SwaggerDriver.Swagger
{
    [DataContract]
    public class ApiDocumentation : Documentation
    {
        [DataMember(Name = "resourcePath")]
        public string DocPath { get; set; }

        [DataMember(Name = "apis")]
        public List<Api> Apis { get; set; }
    }
}