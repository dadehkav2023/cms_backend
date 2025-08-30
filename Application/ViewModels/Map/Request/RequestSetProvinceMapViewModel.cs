using Common.Enum;

namespace Application.ViewModels.Map.Request
{
    public class RequestSetProvinceMapViewModel
    {
        public ProvinceEnum Province { get; set; }
        public string WebsiteAddress { get; set; }
        public string Description { get; set; }
    }
}