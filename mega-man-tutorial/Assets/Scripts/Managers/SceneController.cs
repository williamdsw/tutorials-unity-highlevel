using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Image fade;
        [SerializeField] private GameObject loadText;
        private string nextScene;

        private static SceneController instance;

        public static SceneController Instance { get => instance; private set => instance = value; }

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadScene(string scene)
        {
            nextScene = scene;
            StartCoroutine(FadeIn());
            Invoke("LoadNextScene", 1f);
        }

        public void RestartScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        public void StartScene()
        {
            loadText.SetActive(false);
            StartCoroutine(FadeOut());
        }

        public void LoadNextScene()
        {
            SceneManager.LoadSceneAsync(nextScene);
        }

        private IEnumerator FadeIn()
        {
            Color color = fade.color;
            while (color.a < 1f)
            {
                color.a += Time.deltaTime;
                fade.color = color;
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            if (fade.color.a >= 1f)
            {
                loadText.SetActive(true);
            }
        }

        private IEnumerator FadeOut()
        {
            Color color = fade.color;
            while (color.a > 0)
            {
                color.a -= Time.deltaTime;
                fade.color = color;
                yield return null;
            }
        }
    }
}