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

        private static Stopwatch m_Stopwatch;

        ////Stopwatch提供一组方法和属性，可用于准确地测量运行时间
        private static Stopwatch Stopwatch
        {
            get
            {
                if (m_Stopwatch == null)
                    m_Stopwatch = new Stopwatch();
                return m_Stopwatch;
            }
        }

        /// <summary>
        /// 重置0并且重新开始计时
        /// </summary>
        public static void Start()
        {
            Stopwatch.Restart();
        }

        /// <summary>
        /// 重置0
        /// </summary>
        public static void Reset()
        {
            Stopwatch.Reset();
        }

        /// <summary>
        /// 继续计时（不重置0）
        /// </summary>
        public static void Keep()
        {
            Stopwatch.Start();
        }


        public static TimeSpan Stop()
        {
            Stopwatch.Stop();
            return Stopwatch.Elapsed;
        }

        public static TimeSpan Elapsed
        {
            get
            {
                return Stopwatch.Elapsed;
            }
        }
        public static double TotalMilliseconds
        {
            get
            {
                //return sw.Elapsed.TotalMilliseconds;
                return Stopwatch.ElapsedMilliseconds;
            }
        }
        public static double Totalseconds
        {
            get
            {
                return Stopwatch.Elapsed.TotalSeconds;
            }
        }



    }
}
