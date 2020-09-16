using System;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Codingriver
{
    /// <summary>
    /// 字节辅助工具，System.BitConverter;
    ///   这里是大端模式读写的（BitConverter.IsLittleEndian检查大小端）
    ///   特别注意： C# char类型是双字节的！！！
    /// </summary>
	public static class ByteHelper
    {
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }

        public static string ToHex(this byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder(bytes.Length*2);
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("X2"));
            }
            return stringBuilder.ToString();
        }
        public static string ToHex(this byte[] bytes,int index,int count)
        {
            StringBuilder stringBuilder = new StringBuilder(count * 2);
            for (int i = 0; i < count; i++)
            {
                stringBuilder.Append(bytes[index+i].ToString("X2"));
            }
            return stringBuilder.ToString();
        }

        public static string ToHex(this byte[] bytes, string format)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in bytes)
            {
                stringBuilder.Append(b.ToString(format));
            }
            return stringBuilder.ToString();
        }

        public static string ToHex(this byte[] bytes, int offset, int count)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = offset; i < offset + count; ++i)
            {
                stringBuilder.Append(bytes[i].ToString("X2"));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes">UTF-16 BE,大头方式</param>
        /// <returns></returns>
        public static string ToStr(this byte[] bytes)
        {
            if(bytes.Length%2!=0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", bytes.ToHex()));
            }

            StringBuilder builder = new StringBuilder(bytes.Length / 2);
            for (int i = 0; i < bytes.Length; i+=2)
            {
                ushort value = (ushort)((bytes[i + 1] << 8) | bytes[i]);
                builder.Append((char)value);
            }
            return builder.ToString();
        }
        public static string ToStr(this byte[] bytes, int index, int count)
        {
            if (count % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", bytes.ToHex()));
            }

            StringBuilder builder = new StringBuilder(count / 2);
            for (int i = 0; i < count; i += 2)
            {
                ushort value = (ushort)(( bytes[index + i + 1] << 8) | bytes[index + i]);
                builder.Append((char)value);
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
        //public static string ToStr(this byte[] bytes, int index, int count)
        //{
        //    return Encoding.Default.GetString(bytes, index, count);
        //}

        public static string Utf8ToStr(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static string Utf8ToStr(this byte[] bytes, int index, int count)
        {
            return Encoding.UTF8.GetString(bytes, index, count);
        }
        public static void WriteTo(this byte[] bytes, int offset, ulong num)
        {
            for (int i = 0; i < sizeof(ulong); i++)
                bytes[offset + i] = (byte)(num >> (i * 8) & 0xff);
        }
        public static void ReadTo(this byte[] bytes, int offset, out ulong num)
        {
            num = 0;
            for (int i = sizeof(ulong) - 1; i >= 0; i--)
                num = (num << 8) | bytes[offset + i];
        }

        public static void WriteTo(this byte[] bytes, int offset, long num)
        {
            for (int i = 0; i < sizeof(long); i++)
                bytes[offset + i] = (byte)(num >> (i * 8) & 0xff);
        }
        public static void ReadTo(this byte[] bytes, int offset, out long num)
        {
            num = 0;
            for (int i = sizeof(long) - 1; i >= 0; i--)
                num = (num << 8) | bytes[offset + i];
        }


        public static void WriteTo(this byte[] bytes, int offset, uint num)
        {
            bytes[offset] = (byte)(num & 0xff);
            bytes[offset + 1] = (byte)((num & 0xff00) >> 8);
            bytes[offset + 2] = (byte)((num & 0xff0000) >> 16);
            bytes[offset + 3] = (byte)((num & 0xff000000) >> 24);
        }

        public static void ReadTo(this byte[] bytes, int offset, out uint num)
        {
            num = 0;
            num = bytes[offset + 3];
            num = (uint)((num << 8) | bytes[offset + 2]);
            num = (uint)((num << 8) | bytes[offset + 1]);
            num = (uint)((num << 8) | bytes[offset]);
        }

        public static void WriteTo(this byte[] bytes, int offset, int num)
        {
            bytes[offset] = (byte)(num & 0xff);
            bytes[offset + 1] = (byte)((num & 0xff00) >> 8);
            bytes[offset + 2] = (byte)((num & 0xff0000) >> 16);
            bytes[offset + 3] = (byte)((num & 0xff000000) >> 24);
        }
        public static void ReadTo(this byte[] bytes, int offset, out int num)
        {
            num = 0;
            num = bytes[offset + 3];
            num = (int)((num << 8) | bytes[offset + 2]);
            num = (int)((num << 8) | bytes[offset + 1]);
            num = (int)((num << 8) | bytes[offset]);
        }

        public static void WriteTo(this byte[] bytes, int offset, byte num)
        {
            bytes[offset] = num;
        }

        public static void WriteTo(this byte[] bytes, int offset, short num)
        {
            bytes[offset] = (byte)(num & 0xff);
            bytes[offset + 1] = (byte)((num & 0xff00) >> 8);
        }
        public static void ReadTo(this byte[] bytes, int offset, out short num)
        {
            num = 0;
            num = bytes[offset + 1];
            num = (short)((num << 8) | bytes[offset]);
        }

        public static void WriteTo(this byte[] bytes, int offset, ushort num)
        {
            bytes[offset] = (byte)(num & 0xff);
            bytes[offset + 1] = (byte)((num & 0xff00) >> 8);
        }
        public static void ReadTo(this byte[] bytes, int offset, out ushort num)
        {
            num = 0;
            num = bytes[offset + 1];
            num = (ushort)((num << 8) | bytes[offset]);
        }

        

        public unsafe static void WriteTo(this byte[] bytes, int offset, float num)
        {
            byte* ptr = (byte*)&num;
            for (int i = 0; i < sizeof(float); i++)
                bytes[offset + i] = (byte)(*(ptr + i));
        }
        public unsafe static void ReadTo(this byte[] bytes, int offset, out  float num)
        {
            float tmp = 0;
            byte* ptr = (byte*)&tmp;
            for (int i = sizeof(float) - 1; i >= 0; i--)
                *(ptr + i) = (byte)bytes[offset + i];
            num = tmp;
        }
        public unsafe static void WriteTo(this byte[] bytes, int offset, double num)
        {
            byte* ptr = (byte*)&num;
            for (int i = 0; i < sizeof(double); i++)
                bytes[offset + i] = (byte)(*(ptr + i));
        }
        public unsafe static void ReadTo(this byte[] bytes, int offset, out double num)
        {
            double tmp = 0;
            byte* ptr = (byte*)&tmp;
            for (int i = sizeof(double) - 1; i >= 0; i--)
                *(ptr + i) = (byte)bytes[offset + i];
            num = tmp;
        }
    }
}