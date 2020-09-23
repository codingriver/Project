using System;
using UnityEngine;
namespace Codingriver
{
    public abstract class Singleton_CSharp<T>
        where T : class, new()

    {
        private static T m_Instance;
        private static object locker = new object();

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (locker)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new T();
                        }
                    }
                }
                return m_Instance;
            }
        }

        protected Singleton_CSharp()
        {
            if (m_Instance != null)
            {
                throw new InvalidOperationException("Can't create singleton instance more than once.");
            }
        }


    }
}
