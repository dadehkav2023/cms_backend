using Common.Enum;

namespace Application.ViewModels.Map.Response
{
    public class ResponseGetProvinceMapViewModel
    {
        public ResponseGetProvinceViewModel Province { get; set; }
        public string Description { get; set; }
        public string WebsiteAddress { get; set; }
    }
}