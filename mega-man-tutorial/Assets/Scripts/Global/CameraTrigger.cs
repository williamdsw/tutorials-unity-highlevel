using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private GameObject targetCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.Instance.EnableCamera(targetCamera);
        }
    }
}
