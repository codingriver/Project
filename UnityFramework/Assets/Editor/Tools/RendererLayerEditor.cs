using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEditorInternal;


public class RendererLayerEditor
{
    public static void Register()
    {

        // MeshRendererEditor
        Type type = typeof(AssetDatabase).Assembly.GetType("UnityEditor.MeshRendererEditor");
        MethodInfo method = type.GetMethod("OnInspectorGUI", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        type = typeof(RendererLayerEditor);
        MethodInfo methodReplacement = type.GetMethod("SubRendererOnInspectorGUI", BindingFlags.Static | BindingFlags.NonPublic);
        MethodInfo methodProxy = type.GetMethod("SubRendererOnInspectorGUIProxy", BindingFlags.Static | BindingFlags.NonPublic);
        MethodHook hooker = new MethodHook(method, methodReplacement, methodProxy);
        hooker.Install();

        // SkinnedMeshRendererEditor
        type = typeof(AssetDatabase).Assembly.GetType("UnityEditor.SkinnedMeshRendererEditor");
        method = type.GetMethod("OnInspectorGUI", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        type = typeof(RendererLayerEditor);
        methodReplacement = type.GetMethod("SubRendererOnInspectorGUI", BindingFlags.Static | BindingFlags.NonPublic);
        methodProxy = type.GetMethod("SubRendererOnInspectorGUIProxyE", BindingFlags.Static | BindingFlags.NonPublic);
        hooker = new MethodHook(method, methodReplacement, methodProxy);
        hooker.Install();
    }

    static void SubRendererOnInspectorGUI(Editor editor)
    {
        //Debug.Log(editor.target);
        if (editor.target != null)
        {

            var renderer = editor.target as Renderer;

            var options = GetSortingLayerNames();
            var picks = new int[options.Length];
            //Debug.Log($"renderer.sortingLayerName:{renderer.sortingLayerName},options.len:{options.Length}");
            var name = renderer.sortingLayerName;
            var choice = -1;
            for (int i = 0; i < options.Length; i++)
            {
                picks[i] = i;
                if (name == options[i]) choice = i;
            }

            choice = EditorGUILayout.IntPopup("Sorting Layer", choice, options, picks);
            renderer.sortingLayerName = options[choice];
            renderer.sortingOrder = EditorGUILayout.IntField("Sorting Order", renderer.sortingOrder);
            if(editor.target is MeshRenderer)
            {
                SubRendererOnInspectorGUIProxy(editor);
            }
            else 
            {
                SubRendererOnInspectorGUIProxyE(editor);
            }
            
        }
        

    }

    static void SubRendererOnInspectorGUIProxy(Editor editor)
    {

    }
    static void SubRendererOnInspectorGUIProxyE(Editor editor)
    {

    }
    public static string[] GetSortingLayerNames()
    {
        //SortingLayer.layers
        Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        return (string[])sortingLayersProperty.GetValue(null, new object[0]);
    }
}
