#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace CyberDominion.Editor
{
    public static class WindowsBuildMenu
    {
        private const string ScenePath = "Assets/Scenes/CyberDominion.unity";
        private const string OutputPath = "Builds/Windows/CyberDominion.exe";

        [MenuItem("Cyber Dominion/Gerar jogo para Windows")]
        public static void BuildWindowsGame()
        {
            if (!File.Exists(ScenePath))
            {
                EditorUtility.DisplayDialog(
                    "Cyber Dominion",
                    "A cena principal ainda não foi criada. Aguarde a compilação e use Cyber Dominion > Recriar cena principal.",
                    "OK");
                return;
            }

            Directory.CreateDirectory("Builds/Windows");
            BuildPlayerOptions options = new BuildPlayerOptions
            {
                scenes = new[] { ScenePath },
                locationPathName = OutputPath,
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                EditorUtility.RevealInFinder(OutputPath);
                EditorUtility.DisplayDialog(
                    "Build concluída",
                    "O jogo foi criado em Builds/Windows. Abra CyberDominion.exe para jogar.",
                    "OK");
            }
            else
            {
                EditorUtility.DisplayDialog(
                    "Falha na build",
                    "Verifique os erros apresentados no Console do Unity.",
                    "OK");
            }
        }
    }
}
#endif
