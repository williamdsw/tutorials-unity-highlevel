using UnityEngine;

namespace BeatEmUpTutorial
{
    public class ControladorMusica : MonoBehaviour
    {
        [SerializeField] private AudioClip musicaFase;
        [SerializeField] private AudioClip musicaChefe;
        [SerializeField] private AudioClip musicaFinalizada;
        private AudioSource audioSource;

        public AudioClip MusicaFase => musicaFase;
        public AudioClip MusicaChefe => musicaChefe;
        public AudioClip MusicaFinalizada => musicaFinalizada;

        private void Awake()
        {
            audioSource = this.GetComponent<AudioSource>();
        }

        private void Start()
        {
            PlaySong(MusicaFase);
        }

        public void PlaySong(AudioClip audioClip)
        {
            audioSource.clip = audioClip;

            if (audioClip == MusicaFinalizada)
            {
                audioSource.loop = false;
            }

            audioSource.Play();
        }
    }
}