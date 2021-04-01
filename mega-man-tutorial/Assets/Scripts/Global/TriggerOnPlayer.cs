using UnityEngine;
using UnityEngine.Events;

namespace Global
{
    public class TriggerOnPlayer : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnTrigger;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnTrigger.Invoke();
            }
        }
    }
}