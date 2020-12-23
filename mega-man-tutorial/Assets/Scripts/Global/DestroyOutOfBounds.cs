using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
