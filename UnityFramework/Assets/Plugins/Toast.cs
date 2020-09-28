using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast
{

    static AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    static AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    static AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");


    public static void showToast(string content)
    {

        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
            AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", content);
            AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
            toast.Call("show");
        }
     ));
    }


    
}