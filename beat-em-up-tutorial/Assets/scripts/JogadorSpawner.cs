using UnityEngine;

public class JogadorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabPlayer;
    private GameManager gameManager;

    private void Awake ()
    {
        gameManager = FindObjectOfType<GameManager> ();
        Instantiate (prefabPlayer[gameManager.PlayerIndex], transform.position, transform.rotation);
    }
}