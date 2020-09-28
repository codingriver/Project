using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class AndroidProfilerSetEditor
{

    [MenuItem("Build Tools/AndroidProfiler Reset")]
    /// <summary>
    /// Android手机通过数据线连接Unity，需要重置连接，然后在Unity Console中显示log，Unity Profiler可以连接手机。（适用于PC；mac没有测试）
    /// 打包时需要勾选(PlayerSetting-->Enable Internal Profiler),(BuildSetting-->Development Build),(BuildSetting-->Autoconnect Profiler)。
    /// 设备链接后电脑后启动app，然后点击Reset。
    /// 需要配置adb的环境变量
    /// </summary>
    static void AndroidProfilerReset()
    {
        string bundleId = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
        Debug.Log("bundleId:"+bundleId);
        if(!string.IsNullOrEmpty(bundleId))
        {
            string command = "adb forward tcp:34999 localabstract:Unity-" + bundleId;
            ProcessTask.RunCmdAsync(command);
        }
    }
}
