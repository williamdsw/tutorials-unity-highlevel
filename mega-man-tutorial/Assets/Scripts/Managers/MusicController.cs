using System.Collections;
using UnityEngine;

namespace Managers
{
    public class MusicController : MonoBehaviour
    {
        private AudioSource audioSource;
        private static MusicController instance;

        public static MusicController Instance { get => instance; private set => instance = value; }

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySong(AudioClip song)
        {
            StopAllCoroutines();
            audioSource.volume = 0.5f;
            audioSource.clip = song;
            audioSource.Play();
        }

        public void Stop()
        {
            audioSource.Stop();
        }

        public void FadeSong()
        {
            StartCoroutine(Fading());
        }

        private IEnumerator Fading()
        {
            while (audioSource.volume > 0f)
            {
                audioSource.volume -= (Time.deltaTime / 2);
                yield return null;
            }
        }
    }
}