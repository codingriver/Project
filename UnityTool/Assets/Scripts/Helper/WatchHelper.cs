using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codingriver
{
    /// <summary>
    /// 代码耗时分析
    /// </summary>
    public static class WatchHelper
    {
        private static readonly Stopwatch sw = new Stopwatch(); //Stopwatch提供一组方法和属性，可用于准确地测量运行时间

        /// <summary>
        /// 重置0并且重新开始计时
        /// </summary>
        public static void Start()
        {
            sw.Restart();
        }

        /// <summary>
        /// 重置0
        /// </summary>
        public static void Reset()
        {
            sw.Reset();
        }

        /// <summary>
        /// 继续计时（不重置0）
        /// </summary>
        public static void Keep()
        {
            sw.Start();
        }


        public static TimeSpan Stop()
        {
            sw.Stop();
            return sw.Elapsed;
        }

        public static TimeSpan Elapsed
        {
            get
            {
                return sw.Elapsed;
            }
        }
        public static double TotalMilliseconds
        {
            get
            {
                //return sw.Elapsed.TotalMilliseconds;
                return sw.ElapsedMilliseconds;
            }
        }
        public static double Totalseconds
        {
            get
            {
                return sw.Elapsed.TotalSeconds;
            }
        }



    }
}
