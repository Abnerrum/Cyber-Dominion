#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberDominion.Editor
{
    [InitializeOnLoad]
    public static class ProjectBootstrapper
    {
        private const string ScenePath = "Assets/Scenes/CyberDominion.unity";

        static ProjectBootstrapper()
        {
            EditorApplication.delayCall += EnsurePlayableScene;
        }

        [MenuItem("Cyber Dominion/Recriar cena principal")]
        public static void RebuildScene()
        {
            CreateScene(true);
        }

        [MenuItem("Cyber Dominion/Abrir cena principal")]
        public static void OpenScene()
        {
            if (!File.Exists(ScenePath)) CreateScene(false);
            EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        }

        private static void EnsurePlayableScene()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;
            if (!File.Exists(ScenePath)) CreateScene(false);

            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
        }

        private static void CreateScene(bool askBeforeReplacing)
        {
            if (askBeforeReplacing && File.Exists(ScenePath))
            {
                bool confirmed = EditorUtility.DisplayDialog(
                    "Cyber Dominion",
                    "Recriar a cena remove alterações manuais feitas nela. Continuar?",
                    "Recriar",
                    "Cancelar");
                if (!confirmed) return;
            }

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject root = new GameObject("Cyber Dominion");
            root.AddComponent<GameBootstrap>();

            Directory.CreateDirectory("Assets/Scenes");
            EditorSceneManager.SaveScene(scene, ScenePath);
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Cyber Dominion: cena principal criada. Pressione Play para iniciar.");
        }
    }
}
#endif
