using UnityEngine;

namespace Global
{
    public class DestroyOutOfBounds : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}