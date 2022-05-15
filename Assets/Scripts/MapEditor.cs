#if (UNITY_EDITOR) 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GenMap))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GenMap map = (GenMap)target;
        if (GUILayout.Button("Generate"))
            map.genMap();
        if (GUILayout.Button("Generate Obstacles"))
            map.SpawnObstacle();
    }
}
#endif