using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private static PlayerWeapon instance;
    [SerializeField] private Weapon current;
    [SerializeField] private float energy;
    private bool isTimeStopped;

    private PlayerShoot playerShoot;
    private SpriteRenderer spriteRenderer;

    public static PlayerWeapon Instance { get => instance; private set => instance = value; }
    public bool IsTimeStopped { get => isTimeStopped; set => isTimeStopped = value; }
    public float Energy { get => energy; set => energy = value; }
    public Weapon Current { get => current; set => current = value; }

    private void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Instance = this;
        SelectWeapon(GameManager.Instance.Weapons[0]);
    }

    private void Update()
    {
        if (Current.Type == Weapons.TimeStopper)
        {
            Energy -= (Time.deltaTime / 4f);
            UIManager.Instance.UpdateEnergyBar(Energy);
            if (energy <= 0) {
                SelectWeapon(GameManager.Instance.Weapons[0]);
            }
        }
    }

    public void SelectWeapon(Weapon newWeapon)
    {
        Time.timeScale = 1;
        isTimeStopped = false;
        Current = newWeapon;

        playerShoot.PrefabBullet = newWeapon.WeaponShot;
        spriteRenderer.color = newWeapon.SpriteColor;

        if (Current.Type == Weapons.TimeStopper)
        {
            isTimeStopped = true;
            Time.timeScale = 0.25f;
        }
    }
}
