using UnityEngine;

public class ArmaColetavel : MonoBehaviour
{
    [SerializeField] private ArmaItem weapon;
    private SpriteRenderer spriteRenderer;

    public ArmaItem Weapon { get => weapon; set => weapon = value; }

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = Weapon.Sprite;
        spriteRenderer.color = Weapon.Color;
    }
}