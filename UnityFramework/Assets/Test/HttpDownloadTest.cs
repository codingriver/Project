using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codingriver;
using System;
using System.IO;
using System.Text;
using System.Web;
using UnityEngine.Networking;

public class HttpDownloadTest : MonoBehaviour
{


    // Start is called before the first frame update
    IEnumerator Start()
    {
        
        
        string path = @"F:\url.txt";
        string[] arr = File.ReadAllLines(path);
        for (int i = 0; i < arr.Length; i++)
        {
            string url = arr[i];
            url = URLHelper.UrlEncode(url);
            Debug.Log(url);
            UnityWebRequest head = UnityWebRequest.Head(url);
            yield return head;
            long len;
            long.TryParse(head.GetResponseHeader("Content-Length"),out len);
            Debug.Log("---"+len);
            continue;
            UnityWebRequest webRequest = new UnityWebRequest(url,UnityWebRequest.kHttpVerbGET);
            //webRequest.downloadHandler = new DownloadHandlerFile(filePath);
            yield return webRequest.SendWebRequest();
            Debug.Log(webRequest.responseCode);
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError("Error:::"+ webRequest.isNetworkError.ToString());
                Debug.LogError("Error:::" + webRequest.isHttpError.ToString());
                Debug.LogError("Error:::" + webRequest.ToString());
                //Debug.LogError($"{i}>>{filePath}>>{url}");
            }

        }

        yield break;
    }





}
