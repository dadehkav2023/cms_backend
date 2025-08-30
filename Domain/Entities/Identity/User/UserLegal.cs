using Common.Enum.User;
using Domain.Attributes;
using Domain.Attributes.Identity;
using Domain.Entities.BaseEntity;
using Microsoft.EntityFrameworkCore;
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
    [Index(nameof(IdentityUserId), IsUnique = true)]
    public class UserLegal : BaseEntityWithIdentityKey
    {
        //public User User { get; set; }
        public int IdentityUserId { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
        /// <summary>
        /// شناسه ملی
        /// </summary>
        [MaxLength(64)]
        public string NationalId { get; set; }

        public UserLegalType CompanyType { get; set; }

        [MaxLength(64)]
        public string EconomicCode { get; set; }

        [MaxLength(64)]
        public string RegistrationNumber { get; set; }

        public DateTime? CompanyRegistrationDate { get; set; }

        [MaxLength(400)]
        public string CompanyRegistrationPlace { get; set; }

        public float WorkExperience { get; set; }
    }
}
