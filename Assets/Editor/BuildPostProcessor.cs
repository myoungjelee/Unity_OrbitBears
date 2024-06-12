using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class BuildPostProcessor : IPostprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPostprocessBuild(BuildReport report)
    {
        // 빌드된 애플리케이션의 디렉토리 경로
        string buildPath = Path.GetDirectoryName(report.summary.outputPath);

        //// 대상 플러그인 디렉토리 경로
        //string targetFolderDir = Path.Combine(buildPath, "Rank Data");

        //// 추가할 파일의 원본 경로
        //string sourceFilePath = Path.Combine("Assets", "Orbbec", "Plugins", "x86_64", "OrbbecSand.dll");

        //// 복사될 파일의 대상 경로
        //string targetFilePath = Path.Combine(targetPluginDir, "OrbbecSand.dll");

        //// 대상 디렉토리 생성
        //Directory.CreateDirectory(targetFolderDir);

        //// 파일 복사 (덮어쓰기 여부를 true로 설정)
        //File.Copy(sourceFilePath, targetFilePath, true);

        // 'Assets/Editor' 폴더 내의 파일들을 'additionalFiles' 폴더로 복사
        string sourceEditorDir = Path.Combine("Assets", "Editor");
        string targetAdditionalFilesDir = Path.Combine(buildPath, "Rank Data");

        // additionalFiles 폴더 생성
        Directory.CreateDirectory(targetAdditionalFilesDir);

        // Assets/Editor 폴더 내의 모든 파일을 복사
        foreach (string file in Directory.GetFiles(sourceEditorDir))
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(targetAdditionalFilesDir, fileName);
            File.Copy(file, destFile, true);
        }
    }
}
