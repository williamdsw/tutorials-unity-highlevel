using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int numberOfLifes = 4;
    [SerializeField] private int playerIndex = 0;
    private static GameManager instance;

    public int NumberOfLifes { get => numberOfLifes; set => numberOfLifes = value; }
    public int PlayerIndex { get => playerIndex; set => playerIndex = value; }

    private void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy (gameObject);
        }

        DontDestroyOnLoad (gameObject);
    }
}