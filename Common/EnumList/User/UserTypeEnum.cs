using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum.User
{
    public enum UserTypeEnum
    {
        [Description("حقیقی")]
        Real = 1,

        [Description("حقوقی")]
        Legal = 2,
    }
}
