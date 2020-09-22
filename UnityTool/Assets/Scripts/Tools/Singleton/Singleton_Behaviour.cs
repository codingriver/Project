using System;
using UnityEngine;
namespace Codingriver
{
    public class SingletonRoot : Singleton_Behaviour<SingletonRoot>
    {

    }

    public abstract class Singleton_Behaviour<T>: MonoBehaviour
        where T : MonoBehaviour

    {
        private static T m_Instance;
        private static object locker = new object();

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    GameObject obj = new GameObject("Singleton_" + typeof(T).ToString());
                    GameObject.DontDestroyOnLoad(obj);
                    m_Instance = obj.AddComponent<T>();
                    obj.transform.SetParent(SingletonRoot.Instance.transform);
                }
                return m_Instance;
            }
        }
    }
}
