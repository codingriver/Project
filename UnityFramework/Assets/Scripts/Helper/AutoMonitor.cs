using System;
using System.Threading;

namespace Codingriver
{
    class AutoMonitor
    {

        private object locker = new object();
        /// <summary>
        /// true:WaitOne不阻塞，只一次
        /// false:WaitOne阻塞
        /// </summary>
        private bool unblock;
        private short blockCount = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialState"></param>
        public AutoMonitor(bool initialState)
        {
            this.unblock = initialState;

        }

        public void Set()
        {
            lock (locker)
            {
                if (blockCount > 0)
                {
                    Monitor.Pulse(locker);
                }
                else
                {
                    unblock = true;
                }
            }


        }

        public void WaitOne()
        {
            if (unblock)
            {
                unblock = false;
                return;
            }

            lock (locker)
            {
                blockCount++;
                Monitor.Wait(locker);
                blockCount--;
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
                if(lockState)
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
            if (unblock)
            {
                unblock = false;
                return isNotTimeOut;
            }

            lock (locker)
            {
                blockCount++;
                isNotTimeOut = Monitor.Wait(locker, millisecondsTimeout);
                blockCount--;
            }
            return isNotTimeOut;
        }

        public void Reset()
        {
            unblock = false;
        }

    }
}
