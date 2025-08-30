using Domain.Attributes;
using Domain.Attributes.ServiceDesk;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.ServiceDesk
{
    [ServiceDeskEntity]
    [Auditable]
    public class ServiceDesk : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }        
        public string ImagePath { get; set; }        
        public string LinkService { get; set; }        
        public bool IsActive { get; set; }        
    }
}