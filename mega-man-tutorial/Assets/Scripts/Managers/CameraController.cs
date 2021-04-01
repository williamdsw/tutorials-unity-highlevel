using System.Collections;
using UnityEngine;

namespace Managers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject[] cameras;
        [SerializeField] private float freezeTime = 1f;

        private static CameraController instance;

        public static CameraController Instance { get => instance; private set => instance = value; }

        private void Awake()
        {
            Instance = this;
        }

        public void EnableCamera(GameObject camera)
        {
            if (camera.activeInHierarchy) return;

            for (int index = 0; index < cameras.Length; index++)
            {
                cameras[index].SetActive(false);
            }

            camera.SetActive(true);
            StartCoroutine(FreezingTime());
        }

        private IEnumerator FreezingTime()
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(freezeTime);
            Time.timeScale = 1;
        }
    }
}