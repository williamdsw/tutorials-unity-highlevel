using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeatEmUpTutorial
{
    public class Chefe : Inimigo
    {
        [SerializeField] private float minTimeSpawnBoomerang = 8f;
        [SerializeField] private float maxTimeSpawnBoomerang = 12f;

        public GameObject prefabBoomerang;
        private ControladorMusica musicController;
        private GerenciadorUI uiManager;

        private void Awake()
        {
            uiManager = FindObjectOfType<GerenciadorUI>();

            Invoke("ThrowBoomerang", Random.Range(minTimeSpawnBoomerang, maxTimeSpawnBoomerang));

            musicController = FindObjectOfType<ControladorMusica>();
            musicController.PlaySong(musicController.MusicaChefe);
        }

        private void ThrowBoomerang()
        {
            if (!isDead)
            {
                animator.SetTrigger("Boomerang");
                GameObject objectBoomerang = Instantiate(prefabBoomerang, transform.position, transform.rotation);
                objectBoomerang.GetComponent<Boomerang>().BossDirection = (isFacingRight ? 1 : -1);
                Invoke("ThrowBoomerang", Random.Range(minTimeSpawnBoomerang, maxTimeSpawnBoomerang));
            }
        }

        private void DefeatedBoss()
        {
            musicController.PlaySong(musicController.MusicaFinalizada);
            float musicLength = (musicController.MusicaFinalizada.length + 2f);
            uiManager.UpdateMessage("Level Clear!");
            Invoke("LoadScene", musicLength);
        }

        private void LoadScene()
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentIndex + 1);
        }
    }
}