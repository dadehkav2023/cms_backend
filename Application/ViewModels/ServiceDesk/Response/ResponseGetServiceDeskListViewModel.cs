using System.Collections.Generic;
using Application.ViewModels.Public;

namespace Application.ViewModels.ServiceDesk.Response
{
    public class ResponseGetServiceDeskListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetServiceDeskViewModel> ServiceDeskList { get; set; }
    }
    
    public class ResponseGetServiceDeskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }        
        public string ImagePath { get; set; }        
        public string Description { get; set; }        
        public string LinkService { get; set; }
        public bool IsActive { get; set; }
    }
    
    
}