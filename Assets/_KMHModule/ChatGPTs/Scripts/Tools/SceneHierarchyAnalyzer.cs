#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Text;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using _KMH_Framework;
using System;
using System.IO;

public static class SceneHierarchyAnalyzerRunner
{
    [MenuItem("Tools/ChatGPTs/Analyze Scene Hierarchy")]
    public static void AnalyzeSceneHierarchy()
    {
        Scene scene = SceneManager.GetActiveScene();
        string outputPath = Path.Combine(Application.dataPath, $"../SceneHierarchy_{scene.name}.txt");

        StringBuilder stringBuilder = new StringBuilder();
        GameObject[] roots = scene.GetRootGameObjects();

        foreach (GameObject root in roots)
        {
            AppendObjectRecursive(stringBuilder, root.transform, 0);
        }

        File.WriteAllText(outputPath, stringBuilder.ToString(), Encoding.UTF8);
        ("📦 씬 계층 출력 완료 : " + outputPath).LogFormat();
    }

    private static void AppendObjectRecursive(StringBuilder stringBuilder, Transform transform, int depth)
    {
        string indent = new string(' ', depth * 2);
        string transformName = transform.name;

        Component[] components = transform.GetComponents<Component>();
        List<string> componentNameList = new List<string>();

        foreach (Component component in components)
        {
            if (component == null)
            {
                continue;
            }

            Type componentType = component.GetType();
            if (componentType == typeof(Transform) ||
                componentType.Name.Contains("RectTransform") ||
                componentType.Name.Contains("CanvasRenderer"))
            {
                continue;
            }

            componentNameList.Add(componentType.Name);
        }

        string compString = componentNameList.Count > 0 ? $" [{string.Join(", ", componentNameList)}]" : "";
        stringBuilder.AppendLine($"{indent}📦 {transformName}{compString}");

        foreach (Transform child in transform)
        {
            AppendObjectRecursive(stringBuilder, child, depth + 1);
        }
    }
}
#endif