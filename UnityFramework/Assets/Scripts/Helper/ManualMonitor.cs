using System;
using System.Threading;

namespace Codingriver
{
    class ManualMonitor
    {

        private object locker = new object();

        private bool unblock;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialState"></param>
        public ManualMonitor(bool initialState)
        {
            unblock = initialState;

        }

        public void Set()
        {
            lock (locker)
            {
                Monitor.PulseAll(locker);
                unblock = true;

            }

        }

        public void WaitOne()
        {
            if (unblock)
            {
                return;
            }

            lock (locker)
            {
                Monitor.Wait(locker);

            }
            /*
                        bool lockState = false;
                        try
                        {
                            Monitor.Enter(obj, ref lockState);

                            Monitor.Wait(obj);
                        }
                        finally
                        {
                            if (lockState)
                            {
                                Monitor.Exit(obj);
                            }
                        }
             */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="millisecondsTimeout">线程进入就绪队列之前等待的毫秒数</param>
        /// <returns></returns>
        public bool WaitOne(int millisecondsTimeout)
        {
            bool isNotTimeOut = true;

            if (!unblock)
            {
                return isNotTimeOut;
            }
            lock (locker)
            {
                isNotTimeOut = Monitor.Wait(locker, millisecondsTimeout);

            }
            return isNotTimeOut;
        }

        public void Reset()
        {
            unblock = false;
        }
    }
}
