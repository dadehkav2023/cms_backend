using Domain.Attributes;
using Domain.Attributes.Slider;
using Domain.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Slider
{
    [Slider]
    [Auditable]
    public class Slider : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string LinkAddress { get; set; }
        public int SortOrder { get; set; }
        public bool CanShow { get; set; }
        public DateTime? StartDateTimeShow { get; set; }
        public DateTime? EndDateTimeShow { get; set; }
    }
}
