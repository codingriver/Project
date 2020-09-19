using System;

namespace Codingriver
{
	public static class TimeHelper
    {
        private const string format = "yyyy-MM-dd HH:mm:ss:fff";
        private const string format1 = "HH:mm:ss:fff";

        /*
         DateTime.Ticks:表示自 0001 年 1 月 1 日午夜 12:00:00 以来已经过的时间的以 100 毫微秒为间隔的间隔数,是一个很大的长整数，单位是 100 毫微秒。
         1 毫秒 = 10^-3 秒，
         1 微秒 = 10^-6 秒，
         1 毫微秒 = 10^-9 秒，
         100 毫微秒 = 10^-7 秒。
         */

        private static readonly DateTime epoch1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// 自 0001 年 1 月 1 日午夜 12:00:00到现在经过多少个100微秒数，
        /// </summary>
        private static readonly long epochTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

        /// <summary>
        /// 客户端时间，单位 100 毫微秒
        /// 自 1970 年 1 月 1 日午夜 12:00:00到现在经过多少个100微秒数，
        /// </summary>
        /// <returns></returns>
        public static long Ticks
        {
            get
            {
                return (DateTime.UtcNow.Ticks - epochTicks);
            }
        }

        /// <summary>
        /// 时间戳，秒(UTC)
        /// 表示自1970年1月1日0时0分0秒到现在的秒数
        /// </summary>
        /// <returns></returns>
		public static long NowSeconds
        {
            get
            {
                return (DateTime.UtcNow.Ticks - epochTicks) / 10000000;
            }
        }

        /// <summary>
        /// 时间戳,毫秒(UTC)
        /// 表示自1970年1月1日0时0分0秒到现在的毫秒数
        /// </summary>
        /// <returns></returns>
        public static long Now
        {
            get
            {
                return (DateTime.UtcNow.Ticks - epochTicks) / 10000;
            }
        }

        /// <summary>
        /// 当前时间（当前时区的）
        /// 格式：yyyy-MM-dd HH:mm:ss:fff
        /// </summary>
        public static string CurDate
        {
            get
            {
                return DateTime.UtcNow.ToLocalTime().ToString(format);
            }
        }
        /// <summary>
        /// 当前时间（当前时区的）
        /// 格式：HH:mm:ss:fff
        /// </summary>
        public static string CurTime
        {
            get
            {
                return DateTime.UtcNow.ToLocalTime().ToString(format1);
            }
        }

        /// <summary>
        /// 时间戳转时间显示
        /// </summary>
        /// <param name="milliseconds">时间戳 毫秒</param>
        /// <returns></returns>
        public static string StampToDateByMilliseconds(long milliseconds)
        {
            return epoch1970.AddMilliseconds(milliseconds).ToLocalTime().ToString(format);
        }
        /// <summary>
        /// 时间戳转时间显示
        /// </summary>
        /// <param name="seconds">时间戳 秒</param>
        /// <returns></returns>
        public static string StampToDateBySeconds(long seconds)
        {
            return epoch1970.AddMilliseconds(seconds).ToLocalTime().ToString(format);
        }

        /// <summary>
        /// 剩余时间格式化，格式化成可读模式
        /// </summary>
        /// <param name="seconds">剩余秒数</param>
        /// <returns></returns>
        public static string FormatTime(int seconds)
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, seconds);
            if (timeSpan.Days > 0)
            {
                string daydesc = "D";
                return string.Format("{0}{1} {2:D2}:{3:D2}:{4:D2}", timeSpan.Days, daydesc,timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds );
            }
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }



        /// <summary>
        /// 剩余天数
        /// </summary>
        /// <param name="seconds">剩余秒数</param>
        /// <returns></returns>
        public static int GetDay(int seconds)
        {
            int days = 0;
            days = seconds / 3600 / 24;
            return days;
        }

    }
}