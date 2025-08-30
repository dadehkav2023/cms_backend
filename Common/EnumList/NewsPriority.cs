using System.ComponentModel;

namespace Common.Enum
{
    public enum NewsPriority
    {
        [Description("کم")]
        Low = 1,
        
        [Description("متوسط")]
        Middle = 2,
        
        [Description("زیاد")]
        High = 3,
        
        [Description("داغ")]
        Hot = 4,
        
        [Description("بحرانی")]
        Critical = 5
    }
}