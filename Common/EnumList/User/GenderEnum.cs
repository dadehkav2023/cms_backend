using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum.User
{
    public enum GenderEnum
    {
        [Description("مرد")]
        Man = 2,

        [Description("زن")]
        Woman = 1,
    }
}
