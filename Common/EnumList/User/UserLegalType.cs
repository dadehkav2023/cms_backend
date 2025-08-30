using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum.User
{
    public enum UserLegalType
    {
        [Description("سهامی عام")]
        PublicStock = 1,

        [Description("سهامی خاص")]
        PrivateStock = 2,

        [Description("مسئولیت محدود")]
        LimitedResponsibility = 3,

        [Description("تضامنی")]
        Solidarity = 4,

        [Description("مختلط غیر سهامی")]
        Mixed = 5,

        [Description("مختلط سهامی")]
        MixedStock = 6,

        [Description("نسبی")]
        Relative = 7,

        [Description("تعاونی")]
        Cooperative = 8,
    }
}
