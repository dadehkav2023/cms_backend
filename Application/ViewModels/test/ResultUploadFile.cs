using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.test
{
    public class ResultUploadFile
    {
        //public string errorMessage { get; set; }
        public List<URLImage> result { get; set; }
    }
    public class URLImage
    {
        public string url { get; set; }
        public string name { get; set; }
        public string size { get; set; }
    }
}
