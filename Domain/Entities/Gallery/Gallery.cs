using Domain.Attributes;
using Domain.Attributes.Gallery;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Gallery
{
    [GalleryEntity]
    [Auditable]
    public class Gallery : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
    }
}