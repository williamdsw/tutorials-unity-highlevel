using UnityEngine;

namespace BeatEmUpTutorial
{
    public class ResetarCamera : MonoBehaviour
    {
        private Animator animator;
        private CameraSeguidora followerCamera;

        private void Awake()
        {
            animator = this.GetComponent<Animator>();
        }

        public void Ativar()
        {
            followerCamera = FindObjectOfType<CameraSeguidora>();
            animator.SetTrigger("Go");
        }

        public void ResetCamera()
        {
            Vector2 current = followerCamera.MaximoXY;
            current.x = 200f;
            followerCamera.MaximoXY = current;
        }
    }
}