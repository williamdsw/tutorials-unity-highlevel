using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BeatEmUpTutorial
{
    public class SelecionarMenu : MonoBehaviour
    {
        [SerializeField] private Image imageAdam;
        [SerializeField] private Image imageAxel;
        [SerializeField] private Animator animatorAdam;
        [SerializeField] private Animator animatorAxel;

        private int indexPlayer;
        private Color color;
        private AudioSource audioSource;
        private GameManager gameManager;

        private void Awake()
        {
            audioSource = this.GetComponent<AudioSource>();
        }

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            indexPlayer = 1;
            color = imageAxel.color;
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontal < 0)
            {
                indexPlayer = 0;
                PlaySound();
            }
            else if (horizontal > 0)
            {
                indexPlayer = 1;
                PlaySound();
            }

            switch (indexPlayer)
            {
                case 0:
                    {
                        imageAdam.color = Color.yellow;
                        animatorAdam.SetBool("Attack", true);
                        imageAxel.color = color;
                        animatorAxel.SetBool("Attack", false);
                        break;
                    }

                case 1:
                    {
                        imageAxel.color = Color.yellow;
                        animatorAxel.SetBool("Attack", true);
                        imageAdam.color = color;
                        animatorAdam.SetBool("Attack", false);
                        break;
                    }

                default: break;
            }

            if (Input.GetButtonDown("Submit"))
            {
                gameManager.PlayerIndex = this.indexPlayer;
                int currentIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentIndex + 1);
            }
        }

        private void PlaySound()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}