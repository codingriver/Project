using System;

namespace Codingriver
{
	public static class TimeHelper
    {
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
        public static long Ticks()
        {
            return (DateTime.UtcNow.Ticks - epochTicks);
        }


        /// <summary>
        /// 客户端时间，秒
        /// 表示自1970年1月1日0时0分0秒到现在的秒数
        /// </summary>
        /// <returns></returns>
		public static long NowSeconds()
		{
			return (DateTime.UtcNow.Ticks - epochTicks) / 10000000;
		}

        /// <summary>
        /// 客户端时间,毫秒
        /// 表示自1970年1月1日0时0分0秒到现在的毫秒数
        /// </summary>
        /// <returns></returns>
        public static long Now()
		{
            return (DateTime.UtcNow.Ticks - epochTicks) / 10000;
        }

        /// <summary>
        /// 时间戳转时间显示
        /// </summary>
        /// <param name="milliseconds">时间戳 毫秒</param>
        /// <returns></returns>
        public static string StampToDate(long milliseconds)
        {
            return epoch1970.AddMilliseconds(milliseconds).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff");
        }
        /// <summary>
        /// 时间戳转时间显示
        /// </summary>
        /// <param name="seconds">时间戳 秒</param>
        /// <returns></returns>
        public static string StampToDateBySeconds(long seconds)
        {
            return epoch1970.AddMilliseconds(seconds).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff");
        }
    }
}