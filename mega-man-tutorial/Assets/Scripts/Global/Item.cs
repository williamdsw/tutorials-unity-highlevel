using Player;
using UnityEngine;

namespace Global
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemType type;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerItems items = other.GetComponent<PlayerItems>();
            if (items)
            {
                if (type == ItemType.Energy)
                {
                    items.UpdateEnergyTanks(1);
                }
                else if (type == ItemType.Health)
                {
                    items.UpdateLifeTanks(1);
                }

                Destroy(gameObject);
            }
        }
    }
}