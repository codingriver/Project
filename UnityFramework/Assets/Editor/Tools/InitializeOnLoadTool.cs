using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEditorInternal;

[InitializeOnLoad] // 最好Editor启动及重新编译完毕就执行
public class InitializeOnLoadTool
{
    static bool register = false;
    
    static InitializeOnLoadTool()
    {
        if (!register)
        {
            RendererLayerEditor.Register();
            register =true;
        }

    }

}
