using System;
using UnityEngine;
using Codingriver;
using System.Text;
using System.IO;
public class IDGeneratorTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        long time = 0;
        time = (long)((DateTime.UtcNow - new DateTime()).TotalMilliseconds);
        Debug.Log($"从0001年开始的时间戳：{time}----{time.ToString("X2")}");
        time = TimeHelper.Now;
        Debug.Log($"从1970年开始的时间戳：{time}----{time.ToString("X2")}");
        time = (long)((DateTime.UtcNow - new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
        Debug.Log($"从2020年开始的时间戳：{time}----{time.ToString("X2")}");
        Debug.Log("===================");

        //yield return new WaitForSeconds(2);
        //yield return new WaitForSeconds(2);
        
        StringBuilder b = new StringBuilder();
        Debug.Log(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff"));
        for (int i = 0; i < 1000000; i++)
        {
            long id = TimeHelper.Now;

            //DateTime dt= Jan1st1970.AddMilliseconds(id);
            //b.Append($"{id}---{dt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff")}\n");
            //b.Append($"{id.ToString("X2")}\n");

        }
        Debug.Log(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff"));
        //yield return new WaitForSeconds(1);
        File.WriteAllText(".time1.log", b.ToString());
        b.Clear();
        
        //yield return new WaitForSeconds(2);
        Debug.Log(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff"));
        for (int i = 0; i < 1000000; i++)
        {
            long id = IdGenerater.GenerateId();
            //b.Append($"{id.ToString("X2")} \n");
            //b.Append($"{id}, {IdGenerater.SnowflakeId.AnalyzeId(id)} \n");
        }
        Debug.Log(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff"));
        //yield return new WaitForSeconds(1);
        Debug.Log(b.ToString());
        File.WriteAllText(".time2.log", b.ToString());
        //yield break;
    }

}
