using Common.Enum.User;
using Domain.Attributes;
using Domain.Attributes.Identity;
using Domain.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Identity.User
{
    [EntityType]
    [Auditable]
    public class UserPotential : BaseEntityWithIdentityKey<int>
    {
        public UserTypeEnum UserType { get; set; }
        [MaxLength(64)]
        public string NationalCode { get; set; }

        [MaxLength(64)]
        public string NationalId { get; set; }

        [MaxLength(64)]
        public string Cellphone { get; set; }

        [MaxLength(10)]
        public string VerificationCode { get; set; }

        public string SecurityStamp { get; set; }
        public DateTimeOffset? SecurityStampExpirationDateTime { get; set; }
        public bool IsConfirmed { get; set; }
    }

}
