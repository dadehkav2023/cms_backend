using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum FileTypeEnum
    {
        [Description("Unknown")]
        Unknown = 0,
        [Description("PDF")]
        Pdf = 1,
        [Description("DOC")]
        Doc = 2,
        [Description("DOCX")]
        Docx = 3,
        [Description("Rar")]
        Rar = 4,
        [Description("Zip")]
        Zip = 5,
        [Description("Mp4")]
        Mp4 = 6,
        [Description("MKV")]
        Mkv = 7,
    }
}
