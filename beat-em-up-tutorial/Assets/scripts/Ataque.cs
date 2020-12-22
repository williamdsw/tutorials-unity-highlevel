using UnityEngine;

public class Ataque : MonoBehaviour
{
    [SerializeField] private int damageValue;
    public int DamageValue { get => damageValue; set => damageValue = value; }

    private void OnTriggerEnter (Collider collider)
    {
        switch (collider.gameObject.layer)
        {
            case 9:
            {
                Jogador player = collider.GetComponent<Jogador> ();
                if (player)
                {
                    player.TakeDamage (DamageValue);
                }

                break;
            }

            case 11:
            {
                Inimigo enemy = collider.GetComponent<Inimigo> ();
                if (enemy)
                {
                    enemy.TakeDamage (DamageValue);
                }

                break;
            }

            default: break;
        }
    }
}