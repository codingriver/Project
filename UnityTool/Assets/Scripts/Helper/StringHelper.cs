using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Codingriver
{
	public static class StringHelper
	{

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns>UTF-16 BE，大头方式</returns>
		public static byte[] ToBytes(this string str)
		{
            byte[] bytes = new byte[str.Length * 2];
            for (int i = 0; i < str.Length; i++)
            {
                bytes[i * 2] = (byte)(((ushort)str[i]) & 0xFF);
                bytes[i * 2+1] = (byte)(((ushort)str[i]>>8) & 0xFF);
            }
			return bytes;
		}
        //public static byte[] ToBytes(this string str)
        //{
        //    byte[] byteArray = Encoding.Default.GetBytes(str);
        //    return byteArray;
        //}
        public static IEnumerable<byte> ToBytesItor(this string str)
        {
            byte[] byteArray = Encoding.Default.GetBytes(str);
            return byteArray;
        }

        public static byte[] ToUtf8(this string str)
	    {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            return byteArray;
        }

        /// <summary>
        /// 字符串转Unicode编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns>UTF-16 BE，大头方式</returns>
        public static string ToUnicode(this string str)
        {
            StringBuilder builder = new StringBuilder(str.Length * 6);
            for (int i = 0; i < str.Length; i++)
            {
                builder.AppendFormat("\\u{0:X4}",(ushort)str[i]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Unicode编码转字符串
        /// </summary>
        /// <param name="unicodeStr">UTF-16 BE，大头方式</param>
        /// <returns></returns>
        public static string FromUnicode(this string unicodeStr)
        {
            if (unicodeStr.Length % 6 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The unicode string cannot have an odd number of digits: {0}", unicodeStr));
            }
            StringBuilder builder = new StringBuilder(unicodeStr.Length/6);

            for (int i = 0; i < unicodeStr.Length; i+=6)
            {
                if(unicodeStr[i]=='\\'&&(unicodeStr[i+1]=='u'|| unicodeStr[i + 1] == 'U'))
                {
                    byte high = (byte)((Parse(unicodeStr[i+2])<<4)| Parse(unicodeStr[i + 3]));
                    byte low= (byte)((Parse(unicodeStr[i + 4]) << 4) | Parse(unicodeStr[i + 5]));
                    ushort value = (ushort)((high << 8) | low);
                    builder.Append((char)value);
                }
            }
            return builder.ToString();
        }
        private static int Parse(char c)
        {
            if (c >= 'a')
                return (c - 'a' + 10) & 0x0f;
            if (c >= 'A')
                return (c - 'A' + 10) & 0x0f;
            return (c - '0') & 0x0f;
        }


        public static byte[] HexToBytes(this string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }
            var hexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < hexAsBytes.Length; index++)
                hexAsBytes[index] = (byte)((Parse(hexString[index*2]) << 4) | Parse(hexString[index * 2+1]));
            
            return hexAsBytes;
        }

        //public static byte[] HexToBytes(this string hexString)
        //{
        //	if (hexString.Length % 2 != 0)
        //	{
        //		throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
        //	}

        //	var hexAsBytes = new byte[hexString.Length / 2];
        //	for (int index = 0; index < hexAsBytes.Length; index++)
        //	{
        //		string byteValue = "";
        //		byteValue += hexString[index * 2];
        //		byteValue += hexString[index * 2 + 1];
        //		hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        //	}
        //	return hexAsBytes;
        //}

        public static string Fmt(this string text, params object[] args)
		{
			return string.Format(text, args);
		}

		public static string ListToString<T>(this List<T> list)
		{
			StringBuilder sb = new StringBuilder();
			foreach (T t in list)
			{
				sb.Append(t);
				sb.Append(",");
			}
			return sb.ToString();
		}

        public static bool IsEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

		public static string MessageToStr(object message)
		{
			return Dumper.DumpAsString(message);
		}
	}
}