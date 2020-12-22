using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GerenciadorUI : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Slider sliderPlayer;
    [SerializeField] private Image imagePlayer;
    [SerializeField] private TextMeshProUGUI textPlayer;
    [SerializeField] private TextMeshProUGUI textLifes;

    [Header("Enemy")]
    [SerializeField] private GameObject enemyUI;
    [SerializeField] private Slider sliderEnemy;
    [SerializeField] private Image imageEnemy;
    [SerializeField] private TextMeshProUGUI textEnemy;
    [SerializeField] private TextMeshProUGUI textMessage;

    [SerializeField] private float timeToShowEnemyUI = 4f;
    private float timerInimigo;

    private Jogador player;
    private GameManager gameManager;

    private void Start ()
    {
        player = FindObjectOfType<Jogador> ();

        sliderPlayer.maxValue = player.MaxLife;
        sliderPlayer.value = sliderPlayer.maxValue;

        if (textPlayer)
        {
            textPlayer.text = player.PlayerName;
        }
        
        imagePlayer.sprite = player.SpritePlayer;

        UpdateNumberOfLifes ();
    }

    private void Update ()
    {
        timerInimigo += Time.deltaTime;

        if (timerInimigo >= timeToShowEnemyUI)
        {
            enemyUI.SetActive (false);
            timerInimigo = 0;
        }
    }

    public void UpdateLifeBar (int value)
    {
        sliderPlayer.value = value;
    }

    public void UpdateEnemyUI (int totalLife, int actualLife, string name, Sprite portrait)
    {
        sliderEnemy.maxValue = totalLife;
        sliderEnemy.value = actualLife;

        if (!textEnemy)
        {
            var t = GameObject.Find("Inimigo UI");
            if (t)
            {
                textEnemy = t.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                textEnemy.text = name;
            }
        }
        
        imageEnemy.sprite = portrait;

        timerInimigo = 0;
        enemyUI.SetActive (true);
    }

    public void UpdateNumberOfLifes ()
    {
        if (!textLifes)
        {
            var t = GameObject.Find("Jogador UI");
            if (t)
            {
                textLifes = t.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                textLifes.text = string.Concat ("x", gameManager.NumberOfLifes);
            }
        }
    }

    public void UpdateMessage (string text)
    {
        if (!textLifes)
        {
            textMessage = GameObject.Find("Mensagem").GetComponent<TextMeshProUGUI>();
            textMessage.text = text;
        }
    }
}