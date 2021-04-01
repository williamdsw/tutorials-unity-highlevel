using Managers;
using UnityEngine;

namespace Other
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Sprite selected;
        [SerializeField] private Sprite deselected;
        [SerializeField] private StageSelect currentStage;
        [SerializeField] private AudioClip song;

        private bool isConfirmed = false;
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            SceneController.Instance.StartScene();
            MusicController.Instance.PlaySong(song);
            Select(currentStage);
        }

        private void Select(StageSelect newStage)
        {
            if (isConfirmed) return;

            currentStage.Selection.sprite = deselected;
            currentStage = newStage;
            currentStage.Selection.sprite = selected;
        }

        private void OnConfirm()
        {
            if (isConfirmed) return;

            isConfirmed = true;
            audioSource.Play();
            GameManager.Instance.LevelSelected = currentStage.Scene;
            GameManager.Instance.Boss = currentStage.Boss;
            MusicController.Instance.FadeSong();
            SceneController.Instance.LoadScene("Load");
        }

        public void OnUp()
        {
            if (currentStage.Up != null)
            {
                Select(currentStage.Up);
            }
        }

        public void OnRight()
        {
            if (currentStage.Right != null)
            {
                Select(currentStage.Right);
            }
        }

        public void OnDown()
        {
            if (currentStage.Down != null)
            {
                Select(currentStage.Down);
            }
        }

        public void OnLeft()
        {
            if (currentStage.Left != null)
            {
                Select(currentStage.Left);
            }
        }
    }
}