using System.Collections.Generic;

namespace Infrastructure.ExternalApi.ImageServer
{
    public class UploadsDto
    {
        public bool Status { get; set; }
        public List<string> FileNameAddress { get; set; }
    }

}
