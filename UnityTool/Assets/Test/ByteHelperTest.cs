using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codingriver;
using System.IO;
using System;

public class ByteHelperTest : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        short a= 32767;
        short aa = -32767;
        ushort b = 65534;
        int c = 2147483647;
        int cc = -2147483648;
        uint d = 4294967295;
        long e = 9223372036854775806;
        long ee = -9223372036854775807;
        ulong f = 18446744073709551613;
        float g = 3.40282347E+38F;
        float gg = -66.98978f;
        double h = 1.7976931348623157E+308;
        double hh = 89898989.34567891d;
        Debug.Log($"{g.ToString("F8")}  , {h.ToString("F8")} , {hh.ToString("F8")}");
        byte[] bts = new byte[1024];

        bts.WriteTo(0, a);
        bts.WriteTo(2, aa);
        bts.WriteTo(4, b);
        bts.WriteTo(6, c);
        bts.WriteTo(10, cc);
        bts.WriteTo(14, d);
        bts.WriteTo(18, e);
        bts.WriteTo(26, ee);
        bts.WriteTo(34, f);
        bts.WriteTo(42, g);
        bts.WriteTo(46, gg);
        bts.WriteTo(50, h);
        bts.WriteTo(58, hh);

        short ta;
        short taa;
        ushort tb;
        int tc;
        int tcc;
        uint td;
        long te;
        long tee;
        ulong tf;
        float tg;
        float tgg;
        double th;
        double thh;
        
        bts.ReadTo(0, out ta);
        bts.ReadTo(2, out taa);
        bts.ReadTo(4, out tb);
        bts.ReadTo(6, out tc);
        bts.ReadTo(10, out tcc);
        bts.ReadTo(14, out td);
        bts.ReadTo(18, out te);
        bts.ReadTo(26, out tee);
        bts.ReadTo(34, out tf);
        bts.ReadTo(42, out tg);
        bts.ReadTo(46, out tgg);
        bts.ReadTo(50, out th);
        bts.ReadTo(58, out thh);

        Debug.Log($"ReadTo---------------->>>>{ta} , {taa} , {tb} , {tc} , {tcc} , {td} , {te} , {tee} , {tf} , {tg.ToString("F8")} , {tgg.ToString("F8")} , {th.ToString("F8")} , {thh.ToString("F8")} ");

        ta = BitConverter.ToInt16(bts, 0);
        taa = BitConverter.ToInt16(bts, 2);
        tb = BitConverter.ToUInt16(bts, 4);
        tc = BitConverter.ToInt32(bts, 6);
        tcc = BitConverter.ToInt32(bts, 10);
        td = BitConverter.ToUInt32(bts, 14);
        te = BitConverter.ToInt64(bts, 18);
        tee = BitConverter.ToInt64(bts, 26);
        tf= BitConverter.ToUInt64(bts, 34);
        tg = BitConverter.ToSingle(bts, 42);
        tgg = BitConverter.ToSingle(bts, 46);
        th = BitConverter.ToDouble(bts, 50);
        thh = BitConverter.ToDouble(bts, 58);



        Debug.Log($"BitConverter ToValue>>>>{ta} , {taa} , {tb} , {tc} , {tcc} , {td} , {te} , {tee} , {tf} , {tg} , {tgg} , {th} , {thh}  ");

        BitConverter.GetBytes(a).ReadTo(0, out ta);
        BitConverter.GetBytes(aa).ReadTo(0, out taa);
        BitConverter.GetBytes(b).ReadTo(0, out tb);
        BitConverter.GetBytes(c).ReadTo(0, out tc);
        BitConverter.GetBytes(cc).ReadTo(0, out tcc);
        BitConverter.GetBytes(d).ReadTo(0, out td);
        BitConverter.GetBytes(e).ReadTo(0, out te);
        BitConverter.GetBytes(ee).ReadTo(0, out tee);
        BitConverter.GetBytes(f).ReadTo(0, out tf);
        BitConverter.GetBytes(g).ReadTo(0, out tg);
        BitConverter.GetBytes(gg).ReadTo(0, out tgg);
        BitConverter.GetBytes(h).ReadTo(0, out th);
        BitConverter.GetBytes(hh).ReadTo(0, out thh);

        Debug.Log($"BitConverter ToBytes>>>>  {ta} , {taa} , {tb} , {tc} , {tcc} , {td} , {te} , {tee} , {tf} , {tg} , {tgg} , {th} , {thh}  ");

    }
    
}
