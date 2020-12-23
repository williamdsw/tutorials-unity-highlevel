using System;
using UnityEngine;

[Serializable]
public class Equipment
{
    [SerializeField] private string weaponName;
    [SerializeField] private string animation;

    public Equipment() { }

    public Equipment(string weaponName, string animation)
    {
        WeaponName = weaponName;
        Animation = animation;
    }

    public string WeaponName { get => weaponName; set => weaponName = value; }
    public string Animation { get => animation; set => animation = value; }
}
