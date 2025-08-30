using System.Collections.Generic;
using Application.ViewModels.Public;

namespace Application.ViewModels.QuickAccess.Response
{
    public class ResponseGetQuickAccessListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetQuickAccessViewModel> QuickAccessList { get; set; }
    }

    public class ResponseGetQuickAccessViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public bool? IsActive { get; set; }
    }
}