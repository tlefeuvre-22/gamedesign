#if (UNITY_EDITOR) 
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Cell))]
[CanEditMultipleObjects]
public class CellEditor : Editor
{
    SerializedProperty type;
    SerializedProperty height;
    SerializedProperty occupier;

    void OnEnable()
    {
        type = serializedObject.FindProperty("type");
        height = serializedObject.FindProperty("height");
        occupier = serializedObject.FindProperty("occupier");
    }
    public override void OnInspectorGUI()
    {
        Cell c = (Cell)target;
        serializedObject.Update();
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Toggle Chest"))
            c.ToggleChest();
        if (GUILayout.Button("Toggle Gunpowder Barrel"))
            c.Togglebarrel();
        if (GUILayout.Button("Toggle Shield PU"))
            c.ToggleSPowerUP();
        if (GUILayout.Button("Toggle obstacle"))
            c.ToggleObstacle();
        if (GUILayout.Button("Toggle enemy"))
            c.ToggleEnnemy();
    }

}
#endif