using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private string levelSelected;
    [SerializeField] private int boss;

    private static GameManager instance;

    public List<Weapon> Weapons => weapons;

    public static GameManager Instance { get => instance; private set => instance = value; }
    public int Boss { get => boss; set => boss = value; }
    public string LevelSelected { get => levelSelected; set => levelSelected = value; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddWeapon(Weapon weapon)
    {
        for (int index = 0; index < weapons.Count; index++)
        {
            if (weapons[index].Type == weapon.Type) return;
        }

        weapons.Add(weapon);
    }
}
