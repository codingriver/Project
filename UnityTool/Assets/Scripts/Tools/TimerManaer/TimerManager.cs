using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Codingriver
{




    public class TimerManager: Singleton_Behaviour<TimerManager>
    {
        protected struct Timer
        {
            public long Id { get; set; }
            public long Time { get; set; }
            public Action callback { get; set; }
            public TaskCompletionSource<bool> tcs;
        }

        private long Now
        {
            get
            {
                return TimeHelper.Now;
            }
        }
        



        private readonly Dictionary<long, Timer> timers = new Dictionary<long, Timer>();

        /// <summary>
        /// key: time, value: timer id
        /// </summary>
        private readonly MultiMap<long, long> timeId = new MultiMap<long, long>();

        private readonly Queue<long> timeOutTime = new Queue<long>();

        private readonly Queue<long> timeOutTimerIds = new Queue<long>();


        // 记录最小时间，不用每次都去MultiMap取第一个值
        private long minTime;

        public void Update()
        {
            if (this.timeId.Count == 0)
            {
                return;
            }

            long timeNow = Now;

            if (timeNow < this.minTime)
            {
                return;
            }

            foreach (KeyValuePair<long, List<long>> kv in this.timeId.GetDictionary())
            {
                long k = kv.Key;
                if (k > timeNow)
                {
                    minTime = k;
                    break;
                }
                this.timeOutTime.Enqueue(k);
            }

            while (this.timeOutTime.Count > 0)
            {
                long time = this.timeOutTime.Dequeue();
                foreach (long timerId in this.timeId[time])
                {
                    this.timeOutTimerIds.Enqueue(timerId);
                }
                this.timeId.Remove(time);
            }

            while (this.timeOutTimerIds.Count > 0)
            {
                long timerId = this.timeOutTimerIds.Dequeue();
                Timer timer;
                if (!this.timers.TryGetValue(timerId, out timer))
                {
                    continue;
                }
                this.timers.Remove(timerId);
                if(timer.tcs!=null)
                {
                    timer.tcs.SetResult(true);
                }
                else
                {
                    timer.callback?.Invoke();
                }

            }
        }

        public void Remove(long id)
        {
            Timer timer;
            if (!this.timers.TryGetValue(id, out timer))
            {
                return;
            }
            timer.tcs.SetCanceled();
            this.timers.Remove(id);
        }

        /// <summary>
        /// 等待直到某个时间点(毫秒)
        /// </summary>
        /// <param name="stampTime">时间戳，时间点</param>
        /// <param name="id">计时器id</param>
        /// <returns></returns>
        public Task WaitStamp(long stampTime, out long id)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = stampTime, tcs = tcs };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            id = timer.Id;
            return tcs.Task;
        }
        /// <summary>
        /// 等待直到某个时间点(毫秒)
        /// </summary>
        /// <param name="stampTime">时间戳，时间点</param>
        public Task WaitStamp(long stampTime)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = stampTime, tcs = tcs };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            return tcs.Task;
        }

        /// <summary>
        /// 等待直到某个时间点(毫秒)
        /// </summary>
        /// <param name="stampTime">时间戳，时间点</param>
        /// <param name="callback"></param>
        public void WaitStamp(long stampTime, Action callback)
        {
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = stampTime, tcs = null, callback = callback };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
        }
        /// <summary>
        /// 等待直到某个时间点(毫秒)
        /// </summary>
        /// <param name="stampTime">时间戳，时间点</param>
        /// <param name="callback"></param>
        /// <param name="id">计时器id</param>
        public void WaitStamp(long stampTime, Action callback, out long id)
        {
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = stampTime, tcs = null, callback = callback };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            id = timer.Id;
        }

        /// <summary>
        /// 等待时间(毫秒)
        /// </summary>
        /// <param name="milliseconds">毫秒</param>
        /// <param name="id">计时器id</param>
        /// <returns></returns>
        public Task Wait(long milliseconds, out long id)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = Now + milliseconds, tcs = tcs };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            id = timer.Id;
            return tcs.Task;
        }

        /// <summary>
        /// 等待时间(毫秒)
        /// </summary>
        /// <param name="milliseconds">毫秒</param>
        /// <returns></returns>
        public Task Wait(long milliseconds)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = Now + milliseconds, tcs = tcs };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            return tcs.Task;
        }

        /// <summary>
        /// 等待时间(毫秒)
        /// </summary>
        /// <param name="milliseconds">毫秒</param>
        /// <param name="callback"></param>
        public void Wait(long milliseconds, Action callback)
        {
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = Now + milliseconds, tcs = null,callback=callback };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
        }
        /// <summary>
        /// 等待时间(毫秒)
        /// </summary>
        /// <param name="milliseconds">毫秒</param>
        /// <param name="callback"></param>
        /// <param name="id">计时器id</param>
        public void Wait(long milliseconds, Action callback,out long id)
        {
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = Now + milliseconds, tcs = null, callback = callback };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            id = timer.Id;
        }
    }

}