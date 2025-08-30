using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum.Message
{
    public enum SmsPatternEnum
    {
        [Pattern(patternName: "SabakNewRegister")]
        Register = 1,

        [Pattern(patternName: "SabakForgotPassword")]
        ForgotPassword = 2
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class Pattern : Attribute
    {
        private string _patternName { get; }

        public Pattern(string patternName)
        {
            _patternName = patternName;
        }

        public virtual string PatternName => _patternName;
    }
}
