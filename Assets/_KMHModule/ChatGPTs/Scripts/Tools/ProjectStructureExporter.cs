using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using _KMH_Framework;

public class ProjectStructureExporter
{
    [MenuItem("Tools/ChatGPTs/Export Project Folder Structure")]
    public static void ExportFolderStructure()
    {
        string projectPath = Application.dataPath;
        string outputPath = Path.Combine(projectPath, "../ProjectStructure.txt");

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("### 📁 Unity 프로젝트 폴더 구조 ###\n");
        AppendDirectoryInfo(stringBuilder, projectPath, 0);

        File.WriteAllText(outputPath, stringBuilder.ToString(), Encoding.UTF8);
        typeof(ProjectStructureExporter).LogFormat("📦 폴더 구조 출력 완료 : " + outputPath);
    }

    static void AppendDirectoryInfo(StringBuilder sb, string dirPath, int depth)
    {
        string indent = new string(' ', depth * 2);

        // 디렉터리 이름
        string dirName = Path.GetFileName(dirPath);
        sb.AppendLine($"{indent}- 📁 {dirName}/");

        // 하위 파일
        foreach (string file in Directory.GetFiles(dirPath))
        {
            string fileName = Path.GetFileName(file);
            sb.AppendLine($"{indent}  - 📄 {fileName}");
        }

        // 재귀적으로 하위 디렉터리
        foreach (string subDir in Directory.GetDirectories(dirPath))
        {
            AppendDirectoryInfo(sb, subDir, depth + 1);
        }
    }
}