using UnityEngine;

namespace BeatEmUpTutorial
{
    public class Arma : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Color color;
        private Ataque attack;
        private Jogador player;
        private int durability = 0;

        private void Awake()
        {
            spriteRenderer = this.GetComponent<SpriteRenderer>();
            attack = this.GetComponent<Ataque>();
            player = this.GetComponentInParent<Jogador>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            Inimigo enemy = collider.GetComponent<Inimigo>();

            if (enemy)
            {
                durability--;

                if (durability <= 0)
                {
                    spriteRenderer.sprite = null;
                    player.UnableWeapon();
                }
            }
        }

        public void AtivarArma(Sprite sprite, Color color, int durability, int damage)
        {
            spriteRenderer.sprite = sprite;
            spriteRenderer.color = color;
            this.durability = durability;
            attack.DamageValue = damage;
        }
    }
}