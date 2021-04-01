using UnityEngine;

namespace BeatEmUpTutorial
{
    public class DestruirPorTempo : MonoBehaviour
    {
        [SerializeField] private float tempoDestruir;

        private void Start()
        {
            Destroy(gameObject, tempoDestruir);
        }
    }
}