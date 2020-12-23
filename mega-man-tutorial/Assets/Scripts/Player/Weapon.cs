using System;
using UnityEngine;

[Serializable]
public class Weapon
{
    [SerializeField] private Weapons type;
    [SerializeField] private string name;
    [SerializeField] private Color spriteColor;
    [SerializeField] private float energyAmount;
    [SerializeField] private bool canCharge;
    [SerializeField] private Rigidbody2D weaponShot;

    public Weapons Type { get => type; set => type = value; }
    public string Name { get => name; set => name = value; }
    public Color SpriteColor { get => spriteColor; set => spriteColor = value; }
    public float EnergyAmount { get => energyAmount; set => energyAmount = value; }
    public bool CanCharge { get => canCharge; set => canCharge = value; }
    public Rigidbody2D WeaponShot { get => weaponShot; set => weaponShot = value; }
}
