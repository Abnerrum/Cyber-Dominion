using UnityEngine;

namespace CyberDominion
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 6f;
        [SerializeField] private float zoomSpeed = 3f;

        private void Update()
        {
            GameManager game = GameManager.Instance;
            Camera cameraComponent = Camera.main;
            if (game == null || cameraComponent == null || game.State != GameState.City) return;

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            cameraComponent.transform.position += new Vector3(horizontal, vertical, 0f) * movementSpeed * Time.deltaTime;

            float scroll = Input.mouseScrollDelta.y;
            cameraComponent.orthographicSize = Mathf.Clamp(cameraComponent.orthographicSize - scroll * zoomSpeed, 4.5f, 9f);
            Vector3 position = cameraComponent.transform.position;
            position.x = Mathf.Clamp(position.x, -3f, 3f);
            position.y = Mathf.Clamp(position.y, -2f, 2f);
            cameraComponent.transform.position = position;
        }
    }
}
