using System.ComponentModel;

namespace Common.Enum
{
    public enum NewsTypeEnum
    {
        [Description("پوششی")]
        CoverNews = 1,
        
        [Description("تولیدی")]
        ProductionNews = 2,
        
        [Description("دریافتی")]
        ReceivedNews = 3,
        
        [Description("ارسالی")]
        SentNews = 4,
        
        [Description("نقلی")]
        NarratedNews = 5
    }
}