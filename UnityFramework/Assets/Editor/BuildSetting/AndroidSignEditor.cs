using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public class AndroidSignEditor 
{
    public static string keystoreName= System.IO.Path.Combine(Environment.CurrentDirectory, "user.keystore");
    public static string keystorePass = "111222";
    public static string keyaliasName = "tych";
    public static string keyaliasPass = "111222";
    

    [MenuItem("Build Tools/AndroidSign Set")]
    static void SetAndroidSign()
    {
        PlayerSettings.Android.keystoreName = keystoreName;
        PlayerSettings.Android.keystorePass = keystorePass;
        PlayerSettings.Android.keyaliasName = keyaliasName;
        PlayerSettings.Android.keyaliasPass = keyaliasPass;
    }


}
