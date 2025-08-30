using Application.ViewModels.Public;

namespace Application.ViewModels.ImageGallery.Request
{
    public class RequestGetGalleryViewModel : RequestGetListViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}