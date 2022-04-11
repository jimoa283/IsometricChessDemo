using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TTEditor : EditorWindow
{
    [MenuItem("Window/TTEditor", false, 10000)]
    static void ShowWindow()
    {
        GetWindow<TTEditor>();
    }

    private bool isActive;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(5,5,100,50));
        GUILayout.BeginHorizontal();
        GUILayout.Label("编辑器", "HeaderButton");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();        
        GUILayout.BeginVertical(); 
        GUILayout.Space(20);
        GUILayout.Button("Hor", GUILayout.Width(60));
        isActive = EditorGUI.Foldout(new Rect(100, 100, 100, 100), isActive, "S");
        GUILayout.EndVertical();

    }
}
