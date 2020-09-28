using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using UnityEngine;

namespace Codingriver
{
	public static class JsonHelper
	{
		public static string ToJson(object obj)
		{

			return JsonUtility.ToJson(obj);
		}

		public static T FromJson<T>(string str)
		{
			T t = JsonUtility.FromJson<T>(str);
			ISupportInitialize iSupportInitialize = t as ISupportInitialize;
			if (iSupportInitialize != null)
			{
                iSupportInitialize.EndInit();
            }
			

            IDeserializationCallback deserializationCallback = t as IDeserializationCallback;
            if(deserializationCallback!=null)
            {
                deserializationCallback.OnDeserialization(t);
            }
            return t;
		}

		public static object FromJson(Type type, string str)
		{
			object t = JsonUtility.FromJson(str,type);
			ISupportInitialize iSupportInitialize = t as ISupportInitialize;
            if (iSupportInitialize != null)
            {
                iSupportInitialize.EndInit();
            }

            IDeserializationCallback deserializationCallback = t as IDeserializationCallback;
            if (deserializationCallback != null)
            {
                deserializationCallback.OnDeserialization(t);
            }
            return t;
		}

		public static T Clone<T>(T t)
		{
			return FromJson<T>(ToJson(t));
		}
	}
}