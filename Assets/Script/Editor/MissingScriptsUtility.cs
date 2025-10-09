using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// Utility to find and optionally remove missing scripts from GameObjects.
// Open via: Tools/Missing Scripts Utility
public class MissingScriptsUtility : EditorWindow
{
    private Vector2 scroll;
    private List<GameObject> objectsWithMissing = new();
    private int componentsFound;
    private int missingCount;

    [MenuItem("Tools/Missing Scripts Utility")] 
    static void Init()
    {
        GetWindow<MissingScriptsUtility>("Missing Scripts").Refresh();
    }

    void OnGUI()
    {
        EditorGUILayout.HelpBox("Scan the currently loaded scenes for missing (MonoBehaviour) script references.", MessageType.Info);
        if (GUILayout.Button("Scan"))
        {
            Refresh();
        }
        if (objectsWithMissing.Count > 0)
        {
            if (GUILayout.Button("Remove All Missing Components"))
            {
                RemoveAll();
                Refresh();
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField($"GameObjects: {objectsWithMissing.Count} | Missing Components: {missingCount}");
        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (var go in objectsWithMissing)
        {
            EditorGUILayout.ObjectField(go, typeof(GameObject), true);
        }
        EditorGUILayout.EndScrollView();
    }

    private void Refresh()
    {
        objectsWithMissing.Clear();
        componentsFound = 0;
        missingCount = 0;

        var allGOs = FindObjectsOfType<GameObject>(true);
        foreach (var go in allGOs)
        {
            var components = go.GetComponents<Component>();
            componentsFound += components.Length;
            bool added = false;
            foreach (var comp in components)
            {
                if (comp == null)
                {
                    missingCount++;
                    if (!added)
                    {
                        objectsWithMissing.Add(go);
                        added = true;
                    }
                }
            }
        }
    }

    private void RemoveAll()
    {
        Undo.IncrementCurrentGroup();
        int removed = 0;
        foreach (var go in objectsWithMissing)
        {
            var serializedObject = new SerializedObject(go);
            var prop = serializedObject.FindProperty("m_Component");
            for (int i = prop.arraySize - 1; i >= 0; i--)
            {
                var element = prop.GetArrayElementAtIndex(i);
                var component = element.objectReferenceValue as Component;
                if (component == null)
                {
                    prop.DeleteArrayElementAtIndex(i);
                    removed++;
                }
            }
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(go);
        }
        Debug.Log($"Removed {removed} missing components.");
    }
}
