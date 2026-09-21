using UnityEngine;

namespace CyberDominion
{
    [DefaultExecutionOrder(-1000)]
    public sealed class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            EnsureCamera();
            EnsureComponent<SaveManager>();
            EnsureComponent<ResourceManager>();
            EnsureComponent<BuildingManager>();
            EnsureComponent<CombatManager>();
            EnsureComponent<UIManager>();
            EnsureComponent<AudioManager>();
            EnsureComponent<CameraController>();
            EnsureComponent<GameManager>();
        }

        private T EnsureComponent<T>() where T : Component
        {
            T component = GetComponent<T>();
            return component != null ? component : gameObject.AddComponent<T>();
        }

        private static void EnsureCamera()
        {
            if (Camera.main != null) return;

            GameObject cameraObject = new GameObject("Main Camera");
            Camera cameraComponent = cameraObject.AddComponent<Camera>();
            cameraObject.tag = "MainCamera";
            cameraComponent.orthographic = true;
            cameraComponent.orthographicSize = 6.5f;
            cameraComponent.backgroundColor = new Color(0.035f, 0.055f, 0.10f);
            cameraObject.transform.position = new Vector3(0f, 0f, -10f);
        }
    }
}
