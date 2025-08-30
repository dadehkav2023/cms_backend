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
    public class UserReal : BaseEntityWithIdentityKey
    {
        //public User User { get; set; }
        public int IdentityUserId { get; set; }
        /// <summary>
        /// کد ملی
        /// </summary>
        [MaxLength(64)]
        public string NationalCode { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string LastName { get; set; }

        public GenderEnum Gender { get; set; }

        [MaxLength(64)]
        public string IdNumber { get; set; }

        [MaxLength(150)]
        public string FathersName { get; set; }

        public DateTime? BirthDate { get; set; }

        [MaxLength(64)]
        public string IdIssuePlace { get; set; }
    }
}
