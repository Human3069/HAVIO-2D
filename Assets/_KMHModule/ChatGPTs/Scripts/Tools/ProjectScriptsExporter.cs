using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using _KMH_Framework;

public class ProjectScriptsExporter 
{
    [MenuItem("Tools/ChatGPTs/Export All C# Scripts")]
    public static void ExportAllScriptsToText()
    {
        string[] scriptPaths = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);
        string outputPath = Path.Combine(Application.dataPath, "../ProjectScripts.txt");

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("### 🔍 프로젝트 내 전체 C# 스크립트 출력 ###\n");

        foreach (string scriptPath in scriptPaths)
        {
            string fileName = Path.GetFileName(scriptPath);
            string content = File.ReadAllText(scriptPath);

            stringBuilder.AppendLine($"\n===== 📄 {fileName} =====");
            stringBuilder.AppendLine(content);
        }

        File.WriteAllText(outputPath, stringBuilder.ToString(), Encoding.UTF8);
        typeof(ProjectScriptsExporter).LogFormat("📦 모든 C# 스크립트 내용을 출력했습니다 : " + outputPath);
    }
}