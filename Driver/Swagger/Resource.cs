using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SwaggerDriver.Swagger
{
    [DataContract]
    public class Resource
    {
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "operations")]
        public List<Operation> Operations { get; set; }
    }
}
