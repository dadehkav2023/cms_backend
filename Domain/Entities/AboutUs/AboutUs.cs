using Domain.Attributes;
using Domain.Attributes.AboutUs;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.AboutUs
{
    [AboutUsEntityAttribute]
    [Auditable]
    public class AboutUs : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        public string HeaderImage { get; set; }
        public string AboutUsText { get; set; }
        public string Email { get; set; }
    }
}