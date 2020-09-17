using System;
using UnityEngine;
using Codingriver;
using System.Text;

public class URLHelperTest : MonoBehaviour
{
    private void OnEnable()
    {
        string url = "h://+w.b+b d. m:9 0 0/o/t/i.a? key=你好吗？#&好鸭好鸭 ！! ，#，$，&，'，(，)，*，+，,，-，.，/，:，;，=，?，@，_，~，#";
        //string url = "http://www.baidu.com/ s?ie=utf-8&f=8&tn=baidu&wd=临时邮箱";


        Debug.Log(url);
        Debug.Log(URLHelper.UrlEncode(url, encoding: Encoding.UTF8));
        Debug.Log(URLHelper.EncodeURI(url));

        Debug.Log(URLHelper.UrlDecode(URLHelper.EncodeURI(url), Encoding.UTF8));
        Debug.Log(URLHelper.DecodeURI("h://+w.b+b%20d.%20m:9%200%200/o/t/i.a?%20key=%E4%BD%A0%E5%A5%BD%E5%90%97%EF%BC%9F#&%E5%A5%BD%E9%B8%AD%E5%A5%BD%E9%B8%AD%20%EF%BC%81!%20%EF%BC%8C#%EF%BC%8C$%EF%BC%8C&%EF%BC%8C'%EF%BC%8C(%EF%BC%8C)%EF%BC%8C*%EF%BC%8C+%EF%BC%8C,%EF%BC%8C-%EF%BC%8C.%EF%BC%8C/%EF%BC%8C:%EF%BC%8C;%EF%BC%8C=%EF%BC%8C?%EF%BC%8C@%EF%BC%8C_%EF%BC%8C~%EF%BC%8C#"));
        Debug.Log(URLHelper.Escape(url));
        Debug.Log(URLHelper.Unescape("h%3A//+w.b+b%20d.%20m%3A9%200%200/o/t/i.a%3F%20key%3D%u4F60%u597D%u5417%uFF1F%23%26%u597D%u9E2D%u597D%u9E2D%20%uFF01%21%20%uFF0C%23%uFF0C%24%uFF0C%26%uFF0C%27%uFF0C%28%uFF0C%29%uFF0C*%uFF0C+%uFF0C%2C%uFF0C-%uFF0C.%uFF0C/%uFF0C%3A%uFF0C%3B%uFF0C%3D%uFF0C%3F%uFF0C@%uFF0C_%uFF0C%7E%uFF0C%23"));
        Debug.Log(URLHelper.Unescape(URLHelper.Escape(url)));


    }

}

