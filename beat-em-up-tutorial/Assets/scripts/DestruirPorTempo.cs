using UnityEngine;

public class DestruirPorTempo : MonoBehaviour
{
    [SerializeField] private float tempoDestruir;

	private void Start ()
    {
        Destroy (gameObject, tempoDestruir);
	}
}