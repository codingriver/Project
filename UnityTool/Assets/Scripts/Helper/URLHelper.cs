using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;

namespace Codingriver
{
    public static class URLHelper
    {

        /// <summary>
        /// URL编码（不支持空格和字符+转换）
        /// 使用UTF-16
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Escape(string url)
        {
            StringBuilder builder = new StringBuilder(url.Length * 2);
            for (int i = 0; i < url.Length; i++)
            {
                char c = url[i];
                ushort value = url[i];
                if (c >= '0' && c <= '9' || c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                {
                    builder.Append(c);
                    continue;
                }
                switch (c)
                {
                    case '*':
                    case '/':
                    case '@':
                    case '+':
                    case '-':
                    case '.':
                    case '_':
                        builder.Append(c);
                        continue;
                    default:
                        break;
                }
                if(value<= 255)
                {
                    builder.AppendFormat("%{0:X2}",value);
                }
                else
                {
                    builder.AppendFormat("%u{0:X2}{1:X2}", (value & 0xFF00)>>8, value & 0xFF);
                }
            }

            return builder.ToString();
        }
        /// <summary>
        /// URL解码（不支持空格和字符+转换）
        /// 使用UTF-16
        /// </summary>
        /// <param name="escapeUrl"></param>
        /// <returns></returns>
        public static string Unescape(string escapeUrl)
        {
            StringBuilder builder = new StringBuilder(escapeUrl.Length);
            int index = 0;
            int len = escapeUrl.Length;

            while (index < len)
            {
                if (escapeUrl[index] != '%')
                {
                    builder.Append(escapeUrl[index]);
                    index++;
                    continue;
                }
                else if (escapeUrl[index+1] == 'u')
                {
                    // unicode
                    if (index + 5 >= len)
                    {
                        throw new Exception("error url not match");
                    }
                    ushort value = 0;
                    int high = ByteHelper.Parse(escapeUrl[index + 2]);
                    int low = ByteHelper.Parse(escapeUrl[index + 3]);
                    value = (byte)((high << 4) | low);
                    high = ByteHelper.Parse(escapeUrl[index + 4]);
                    low = ByteHelper.Parse(escapeUrl[index + 5]);
                    value = (ushort)((value<<8) |((high << 4) | low));
                    builder.Append((char)value);
                    index = index + 6;
                }
                else
                {
                    // ascii
                    if (index + 2 >= len)
                    {
                        throw new Exception("error url not match");
                    }
                    ushort value = 0;
                    int high = ByteHelper.Parse(escapeUrl[index + 1]);
                    int low = ByteHelper.Parse(escapeUrl[index + 2]);
                    value = (byte)((high << 4) | low);
                    builder.Append((char)value);
                    index = index + 3;
                }

            }

            return builder.ToString();
        }

        /// <summary>
        /// URL编码（支持空格和字符+转换）
        /// 使用UTF-16
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string UrlEncode(string str, Encoding encoding = null)
        {
            if (encoding == null)
                return HttpUtility.UrlEncode(str);
            else
                return HttpUtility.UrlEncode(str, encoding);
        }
        /// <summary>
        /// URL解码（支持空格和字符+转换）
        /// 使用UTF-16
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string UrlDecode(string str, Encoding encoding = null)
        {
            if (encoding == null)
                return HttpUtility.UrlDecode(str);
            else
                return HttpUtility.UrlDecode(str, encoding);
        }

        /// <summary>
        /// URL编码（不支持空格和字符+转换）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding">默认使用UTF-8</param>
        /// <returns></returns>
        public static string EncodeURI(string url, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            StringBuilder builder = new StringBuilder(url.Length * 2);
            char[] cArr = new char[1];
            for (int i = 0; i < url.Length; i++)
            {
                char c = url[i];
                ushort value = url[i];
                if (c >= '0' && c <= '9' || c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                {
                    builder.Append(c);
                    continue;
                }
                switch (c)
                {
                    case '!':
                    case '#':
                    case '$':
                    case '&':
                    case '\'':
                    case '(':
                    case ')':
                    case '*':
                    case '+':
                    case ',':
                    case '/':
                    case ':':
                    case ';':
                    case '=':
                    case '?':
                    case '@':
                    case '-':
                    case '.':
                    case '_':
                    case '~':
                    case '"':
                        builder.Append(c);
                        continue;
                    default:
                        break;
                }
                cArr[0] = c;
                byte[] bts = encoding.GetBytes(cArr);
                for (int j = 0; j < bts.Length; j++)
                {
                    builder.AppendFormat("%{0:X2}", bts[j]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// URL解码（不支持空格和字符+转换）
        /// 
        /// </summary>
        /// <param name="encodeUrl"></param>
        /// <param name="encoding">默认使用UTF-8</param>
        /// <returns></returns>
        public static string DecodeURI(string encodeUrl, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            StringBuilder builder = new StringBuilder(encodeUrl.Length);
            int index = 0;
            int len = encodeUrl.Length;
            List<byte> bytes = new List<byte>(128);

            while (index < len)
            {
                if (encodeUrl[index] == '%')
                {
                    if (index + 2 >= len)
                    {
                        throw new Exception("error url not match");
                    }
                    int high = ByteHelper.Parse(encodeUrl[index + 1]);
                    int low = ByteHelper.Parse(encodeUrl[index + 2]);
                    byte value = (byte)((high << 4) | low);
                    bytes.Add(value);
                    index = index + 3;
                }
                else
                {
                    if (bytes.Count > 0)
                    {
                        builder.Append(encoding.GetString(bytes.ToArray()));
                        bytes.Clear();
                    }
                    builder.Append(encodeUrl[index]);
                    index++;
                    continue;
                }

            }
            if (bytes.Count > 0)
            {
                builder.Append(encoding.GetString(bytes.ToArray()));
                bytes.Clear();
            }

            return builder.ToString();
        }


        /// <summary>
        /// URL编码（不支持空格和字符+转换）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding">默认使用UTF-8</param>
        /// <returns></returns>
        public static string EncodeURIComponent(string url, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            StringBuilder builder = new StringBuilder(url.Length * 2);
            char[] cArr = new char[1];
            for (int i = 0; i < url.Length; i++)
            {
                char c = url[i];
                ushort value = url[i];
                if (c >= '0' && c <= '9' || c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                {
                    builder.Append(c);
                    continue;
                }
                switch (c)
                {
                    case '!':
                    case '\'':
                    case '(':
                    case ')':
                    case '*':
                    case '-':
                    case '.':
                    case '_':
                    case '~':
                        builder.Append(c);
                        continue;
                    default:
                        break;
                }
                cArr[0] = c;
                byte[] bts = encoding.GetBytes(cArr);
                for (int j = 0; j < bts.Length; j++)
                {
                    builder.AppendFormat("%{0:X2}", bts[j]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// URL解码（不支持空格和字符+转换）
        /// </summary>
        /// <param name="encodeUrl"></param>
        /// <param name="encoding">默认使用UTF-8</param>
        /// <returns></returns>
        public static string DecodeURIComponent(string encodeUrl, Encoding encoding = null)
        {
            return DecodeURI(encodeUrl, encoding);
        }
    }
}
