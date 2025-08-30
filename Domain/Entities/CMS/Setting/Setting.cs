using Domain.Attributes;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.CMS.Setting
{
    [EntityTypeAttribute]
    [AuditableAttribute]
    public class Setting : BaseEntityWithIdentityKey
    {
        public string Name { get; set; }
        public string LogoImageAddress { get; set; }
        public string? InstagramAddress { get; set; } = null;
        public string? FacebookAddress { get; set; } = null;
        public string? TelegramAddress { get; set; } = null;
        public string? WhatsappAddress { get; set; } = null;
        public string? TwitterAddress { get; set; } = null;
        public string AboutUsSummary { get; set; } = null;
        public string Tell { get; set; }
        public string? Fax { get; set; } = null;
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string GoogleMapLink { get; set; }
        public string LatitudeAndLongitude { get; set; }
        public int SliderImageCount { get; set; } = 0;
        public int HomePageNewsCount { get; set; } = 0;
    }
}