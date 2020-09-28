using System;






    /// <summary>
    /// 注意字节序 测试
    /// </summary>
    public class FloatHelper
{
        /*
         float S 1-1,P 2-9,M 10-32
         S是符号位，P是阶码，M是尾数
        其中S是符号位，只有0和1，分别表示正负；P是阶码，通常使用移码表示（移码和补码只有符号位相反，其余都一样。对于正数而言，原码，反码和补码都一样；对于负数而言，补码就是其绝对值的原码全部取反，然后加1.）

        阶码 是移码 还是 最大值+当前值 的二进制  ???????
         */
        /// <summary>
        /// 浮点数转uint
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static uint FloatToUInt(float data)
        {
            if (data == 0)
            {
                return 0;
            }
            uint origin = 0;
            uint minus = 0;
            int exponent = 0;
            UInt64 mantissa = 0;
            minus = (uint)(data > 0 ? 0 : 1);
            data = Math.Abs(data);
            if (data > UInt64.MaxValue)
            {
                UnityEngine.Debug.LogError("ERR:: float is exceed UInt64 Max limit!");
                return 0;
            }

            int num = 0;
            if (data < 1)
            {

                //计算小数的二进制 并且保留24位有效二进制数
                int start = 0;
                bool isFirst = false;
                int i = 0;
                while (i < 24 + start)
                {
                    mantissa <<= 1;

                    data *= 2;
                    if (data >= 1)
                    {
                        data -= 1;
                        mantissa += 1;
                        if (isFirst == false)
                        {
                            isFirst = true;
                        }
                    }

                    if (isFirst == false)
                    {
                        start++;
                    }
                    i++;
                }
                int integerLen = 0;
                for (i = 1; i <= 64; i++)
                {
                    if (mantissa >> i == 0)
                    {
                        integerLen = i;
                        break;
                    }
                    if (i == 64)
                    {
                        integerLen = i;
                        break;
                    }

                }

                mantissa = ClearHideOneMantissa(mantissa, integerLen);
                //检查第25位二进制数是否为1， 若是1则向第24位进1
                if (data * 2 >= 1)
                {
                    mantissa += 1;
                }
                num = start + 1;
                exponent = -num;
            }
            else
            {
                mantissa = (UInt64)data;
                data -= mantissa;
                //清除最高位1，该位为隐藏位
                int integerLen = 0;
                for (int i = 1; i <=64; i++)
                {
                    if (mantissa>>i==0)
                    {
                        integerLen = i;
                        break;
                    }
                    if(i==64)
                    {
                        integerLen = i;
                        break;
                    }

                }
                mantissa = ClearHideOneMantissa(mantissa, integerLen);
                exponent = (integerLen - 1);
                //计算小数的二进制 并且和整数部分总共保留24位有效二进制数
                if (exponent > 24)
                {
                    mantissa >>= integerLen - 24;
                }
                for (int i = 0; i < 24 - exponent - 1; i++)
                {
                    mantissa <<= 1;
                    data *= 2;
                    if (data >= 1)
                    {
                        data -= 1;
                        mantissa += 1;
                    }
                }
                //检查第25位二进制数是否为1， 若是1则向第24位进1
                if (data * 2 >= 1)
                {
                    mantissa += 1;
                }
            }
            exponent = exponent + 127;
            origin += minus;
            origin <<= 8;
            origin += (uint)exponent;
            origin <<= 23;
            origin += (uint)mantissa;

            return origin;
        }

        /// <summary>
        /// uint浮点数 转浮点数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static float UIntToFloat(uint data)
        {
            if (data == 0)
            {
                return 0;
            }

            float target = 0;
            int state = (data >> 31) == 1 ? -1 : 1;

            int exponent = (int)(data >> 23);
            uint mantissa = (data ^ ((uint)exponent << 23));
            mantissa ^= 0x800000;
            if (state == -1)
            {
                exponent ^= 0x100;
            }
            exponent -= 127;

            if (exponent < 24)
            {
                for (int j = 0; j < 24; j++)
                {
                    if ((mantissa >> (int)(23 - j) & 0x01) == 1 && exponent - j < 0)
                    {
                        target += (float)(System.Math.Pow(2, exponent - j));
                    }

                }

                if (exponent >= 0)
                {
                    target += mantissa >> (int)(23 - exponent);
                }
            }
            else
            {
                exponent -= 23;
                target += (float)(mantissa * 1.0f * System.Math.Pow(2, exponent));
            }
            target = state * target;

            //Console.WriteLine("UIntToFloat::"+target);
            return target;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] FloatToBytes(float data)
        {
            byte[] target = new byte[4];
            uint temp = FloatToUInt(data);
            //字节序 当前是小端 高位放到数组开头
            for (int i = 0; i < 4; i++)
            {
                target[i] = (byte)(temp >> (3-i) & 0xff);
            }
            return target;
        }

        private static float BytesToFloat(byte[]data)
        {
            uint temp = 0;
            for (int i = 0; i < 4; i++)
            {
                temp = temp << 8;
                temp ^= data[i];
            }
            return UIntToFloat(temp);
        }
        /// <summary>
        /// 清除最高位1，该位为隐藏位
        /// </summary>
        /// <param name="mantissa">原始数据</param>
        /// <param name="target"> 为了取二进制最高位1的编号</param>
        /// <returns></returns>
        private static UInt64 ClearHideOneMantissa(UInt64 mantissa,int len)
        {
            //左移里一个比较特殊的情况是当左移的位数超过该数值类型的最大位数时,编译器会用左移的位数去模类型的最大位数,然后按余数进行移位
            //左移右移都会取模类型的最大位数
            if (len == 1)
            {
                mantissa = 0;
            }
            mantissa <<= (64 - len + 1);
            mantissa >>= (64 - len + 1);
            return mantissa;
        }        
    }

