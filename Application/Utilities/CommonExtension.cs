using DNTPersianUtils.Core;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Application.Utilities
{
    public static class CommonExtension
    {
     public static DateTime MinDate => new DateTime(1907, 1, 1);
        public static DateTime MaxDate => new DateTime(2060, 1, 1);

        public static DateTime ConvertJalaliToMiladi(this string persianDate)
        {
            var timeSpan = new TimeSpan(0, 0, 0, 0);
            var calendar = new PersianCalendar();
            try
            {
                persianDate = persianDate.PersianNumberToLatin();

                if (string.IsNullOrEmpty(persianDate))
                {
                    return DateTime.MinValue;
                }
                persianDate = persianDate.Trim().ToEnglishNumbers();
                persianDate = persianDate.Replace("-", "/");
                persianDate = persianDate.Replace(",", "/");
                persianDate = persianDate.Replace("؍", "/");
                persianDate = persianDate.Replace(".", "/");
                persianDate = persianDate.Replace("-", "/");
                var s = persianDate.Split(' ');
                if (s.Length == 2)
                {
                    timeSpan = TimeSpan.Parse(s[1]);
                }
                var date = s[0];

                var match = Regex.Match(date,
                                     @"(?'Year'(^[1-4]\d{3})|(\d{2}))[/-:](((?'Month'0?[1-6])\/((?'Day'(3[0-1])|([1-2][0-9])|(0?[1-9])))|((?'Month'1[0-2]|(0?[7-9]))\/(?'Day'30|([1-2][0-9])|(0?[1-9])))))$");
                if (!match.Success)
                {
                    throw new Exception("InvalidPersianDate");
                }
                var yearGroup = match.Groups["Year"].ToString();
                if (yearGroup.Length == 2)
                {
                    yearGroup = $"13{yearGroup}";
                }
                var year = yearGroup.SafeInt(0);
                var month = match.Groups["Month"].SafeInt(0);
                var day = match.Groups["Day"].SafeInt(0);
                try
                {
                    return calendar.ToDateTime(year, month, day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                }
                catch (Exception exDate)
                {
                    if (exDate.Message == "Day must be between 1 and 29 for month 12.\r\nParameter name: day")
                        return calendar.ToDateTime(year, month, day - 1, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                    throw new Exception("InvalidPersianDate");
                }
            }
            catch (Exception)
            {
                throw new Exception("InvalidPersianDate");
            }
        }

        public static DateTime ConvertJalaliToMiladi(this string persianDate, string time)
        {
            try
            {
                if (string.IsNullOrEmpty(persianDate))
                {
                    return DateTime.MinValue;
                }
                persianDate = persianDate.Trim().ToEnglishNumbers();
                persianDate = persianDate.Replace("-", "/");
                persianDate = persianDate.Replace(",", "/");
                persianDate = persianDate.Replace("؍", "/");
                persianDate = persianDate.Replace(".", "/");
                persianDate = persianDate.Replace("-", "/");
                var s = persianDate.Split(' ');
                string date;
                TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0);
                if (s.Length == 2)
                {
                    timeSpan = TimeSpan.Parse(s[1]);
                }
                date = s[0];

                Match match = Regex.Match(date,
                                     @"(?'Year'(^[1-4]\d{3})|(\d{2}))[/-:](((?'Month'0?[1-6])\/((?'Day'(3[0-1])|([1-2][0-9])|(0?[1-9])))|((?'Month'1[0-2]|(0?[7-9]))\/(?'Day'30|([1-2][0-9])|(0?[1-9])))))$");
                if (!match.Success)
                {
                    throw new System.Exception("InvalidPersianDate");
                }
                var yearGroup = match.Groups["Year"].ToString();
                if (yearGroup.Length == 2)
                {
                    yearGroup = $"13{yearGroup}";
                }
                int year = yearGroup.SafeInt(0);
                int month = match.Groups["Month"].SafeInt(0);
                int day = match.Groups["Day"].SafeInt(0);
                PersianCalendar calendar = new PersianCalendar();
                try
                {
                    return calendar.ToDateTime(year, month, day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                }
                catch (Exception exDate)
                {
                    if (exDate.Message == "Day must be between 1 and 29 for month 12.\r\nParameter name: day")
                        return calendar.ToDateTime(year, month, day - 1, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                    else
                        return DateTime.MinValue;
                }
            }
            catch (System.Exception)
            {
                throw new Exception("InvalidPersianDate");
            }
        }
        public static DateTime ConvertJalaliToMiladi(int year,int month,int day)
        {
            var calendar = new PersianCalendar();
            var timeSpan = new TimeSpan(0, 0, 0, 0);
            try
            {
                return calendar.ToDateTime(year, month, day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            }
            catch (Exception exDate)
            {
                if (exDate.Message == "Day must be between 1 and 29 for month 12.\r\nParameter name: day")
                    return calendar.ToDateTime(year, month, day - 1, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                throw new Exception("InvalidPersianDate");
            }
        }
        public static string ConvertMiladiToJalali(this DateTime date, bool showTime)
        {
            if ((date <= DateTime.MinValue))
            {
                return "";
            }
            var obj = new PersianCalendar();
            //if (date <= DateTime.MinValue)
            //{
            //    date = new DateTime(622, 3, 21);
            //}
            var day = obj.GetDayOfMonth(date);
            var month = obj.GetMonth(date);
            var year = obj.GetYear(date);
            var hour = obj.GetHour(date);
            var minute = obj.GetMinute(date);
            var second = obj.GetSecond(date);
            var dayStr = obj.GetDayOfMonth(date).CompareTo(10) >= 0 ? day.ToString() : "0" + day;
            var monthStr = obj.GetMonth(date).CompareTo(10) >= 0 ? month.ToString() : "0" + month;
            return showTime ? $"{year}/{monthStr}/{dayStr} {hour}:{minute}:{second}" : $"{year}/{monthStr}/{dayStr}";
        }

        public static string ConvertMiladiToJalali(this DateTime date)
        {
            return ConvertMiladiToJalali(date, false);
        }

        public static string ConvertMiladiToJalali(this DateTime? date)
        {
            return date == null ? string.Empty : ConvertMiladiToJalali((DateTime)date, false);
        }
        
        public static int SafeInt(this object i, int exceptionValue)
        {
            if (i != null)
            {
                int.TryParse(i.SafeString().Split('.')[0], out exceptionValue);
            }

            return exceptionValue;
        }

        public static string SafeString(this object i)
        {
            if (i != null)
            {
                return i.ToString();
            }

            return null;
        }
        public static int SafeInt(this object i)
        {
            return SafeInt(i, -1);
        }
        
        public static string PersianNumberToLatin(this string number)
        {
            if (number is null)
            {
                return null;
            }
            string s = number;
            s =
                s.Replace("\u06F0", "0").Replace("\u06F1", "1").Replace("\u06F2", "2").Replace("\u06F3", "3").Replace(
                    "\u06F4", "4").Replace("\u06F5", "5").Replace("\u06F6", "6").Replace("\u06F7", "7").Replace(
                    "\u06F8", "8").Replace("\u06F9", "9");
            return s;
        }

    }
    
}