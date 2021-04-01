using UnityEngine;

namespace BeatEmUpTutorial
{
    // "ScriptableObject" mantem os dados de varias armas / itens do mesmo tipo
    [CreateAssetMenu]
    public class ArmaItem : ScriptableObject
    {
        [SerializeField] private int durability = 0;
        [SerializeField] private Sprite sprite = null;
        [SerializeField] private Color color;
        [SerializeField] private int damage;

        public int Durability { get => durability; set => durability = value; }
        public Sprite Sprite { get => sprite; set => sprite = value; }
        public Color Color { get => color; set => color = value; }
        public int Damage { get => damage; set => damage = value; }
    }
}