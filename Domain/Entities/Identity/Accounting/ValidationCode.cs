using System;
using Domain.Attributes;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Identity.Accounting
{
    [EntityType]
    [Auditable]
    public class ValidationCode : BaseEntityWithIdentityKey
    {
        public string MobileNumber { get; set; }
        public string VerificationCode { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
        public bool IsConfirmed { get; set; }
    }
}