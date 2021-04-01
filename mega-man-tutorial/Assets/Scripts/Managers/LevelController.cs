using Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject playerIntro;
        [SerializeField] private GameObject introText;
        [SerializeField] private AudioClip stageSong;
        [SerializeField] private AudioClip bossFightSong;
        [SerializeField] private AudioClip victorySong;

        [Header("Checkpoint")]
        [SerializeField] private bool checkpointReached;
        [SerializeField] private Transform checkpointPosition;
        [SerializeField] private GameObject checkpointCamera;

        [SerializeField] private Weapon levelWeapon;
        [SerializeField] private UnityEvent OnStartBossFight;
        [SerializeField] private UnityEvent OnCheckpoint;

        private static LevelController instance;

        public static LevelController Instance { get => instance; private set => instance = value; }
        public bool CheckpointReached { get => checkpointReached; set => checkpointReached = value; }

        private void Awake()
        {
            Instance = this;
        }

        private IEnumerator Start()
        {
            if (SceneController.Instance)
            {
                SceneController.Instance.StartScene();
            }

            if (MusicController.Instance)
            {
                MusicController.Instance.PlaySong(stageSong);
            }

            yield return new WaitForSeconds(3f);
            introText.SetActive(false);
            yield return new WaitForSeconds(0.75f);
            playerIntro.SetActive(false);
            player.SetActive(true);
        }

        public void SetCheckPoint(bool state)
        {
            CheckpointReached = state;
        }

        public void Restart()
        {
            player.transform.position = checkpointPosition.position;
            player.SetActive(true);
            OnCheckpoint.Invoke();
            Invoke("SetCamera", 2f);
        }

        private void SetCamera()
        {
            CameraController.Instance.EnableCamera(checkpointCamera);
            if (MusicController.Instance)
            {
                MusicController.Instance.PlaySong(stageSong);
            }
        }

        public void PlayBossFight()
        {
            if (MusicController.Instance)
            {
                MusicController.Instance.PlaySong(bossFightSong);
            }

            Invoke("StartFight", 1f);
        }

        public void StartFight()
        {
            OnStartBossFight.Invoke();
        }

        public void FinishLevel()
        {
            GameManager.Instance.AddWeapon(levelWeapon);
            StartCoroutine(FinishingLevel());
        }

        private IEnumerator FinishingLevel()
        {
            if (MusicController.Instance)
            {
                MusicController.Instance.FadeSong();
            }

            yield return new WaitForSeconds(3f);

            if (MusicController.Instance)
            {
                MusicController.Instance.PlaySong(victorySong);
            }

            if (SceneController.Instance)
            {
                yield return new WaitForSeconds(5f);
                SceneController.Instance.LoadScene("GetEquipped");
            }
        }
    }
}