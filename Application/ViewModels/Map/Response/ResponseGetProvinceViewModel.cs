using Application.Utilities;
using Common.Enum;
using Common.EnumList;

namespace Application.ViewModels.Map.Response
{
    public class ResponseGetProvinceViewModel
    {
        public ProvinceEnum Province { get; set; }

        public string Description => Province.GetDescription();
    }
}