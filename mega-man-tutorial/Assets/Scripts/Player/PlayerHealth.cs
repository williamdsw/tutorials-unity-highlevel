using Global;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Damageable
    {
        private int defaultLayer;

        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

        protected override void Start()
        {
            base.Start();
            defaultLayer = gameObject.layer;
        }

        public override void Death()
        {
            if (LevelController.Instance.CheckpointReached)
            {
                Debug.Log("Checkpoint");
                LevelController.Instance.Restart();
                Respawn();
            }
            else
            {
                SceneController.Instance.RestartScene();
            }
        }

        public void SetInvencible(bool state)
        {
            if (state)
            {
                UIManager.Instance.UpdateHealthBar(currentHealth);
                gameObject.layer = LayerMask.NameToLayer("Invencible");
            }
            else
            {
                gameObject.layer = defaultLayer;
            }
        }
    }
}