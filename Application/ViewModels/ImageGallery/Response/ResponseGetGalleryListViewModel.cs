using System.Collections.Generic;
using Application.ViewModels.Public;

namespace Application.ViewModels.ImageGallery.Response
{
    public class ResponseGetGalleryListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetGalleryViewModel> GalleryList { get; set; }
    }
    
    public class ResponseGetGalleryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;    
    }
    
    
}