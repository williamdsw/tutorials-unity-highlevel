using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItems : MonoBehaviour
{
    [SerializeField] private int healthTanks;
    [SerializeField] private int energyTanks;

    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void OnHealthUsed(InputAction.CallbackContext callback)
    {
        if (healthTanks <= 0) return;
        healthTanks--;
        UpdateLifeTanks(healthTanks);
        UpdateHealthBar();
    }

    public void OnEnergyUsed(InputAction.CallbackContext callback)
    {
        if (energyTanks <= 0) return;
        energyTanks--;
        UpdateEnergyTanks(energyTanks);
        UpdateEnergyBar();
    }

    public void UpdateEnergyTanks(int amount)
    {
        energyTanks = amount;
        UIManager.Instance.UpdateEnergyTanks(energyTanks);
    }

    public void UpdateLifeTanks(int amount)
    {
        healthTanks = amount;
        UIManager.Instance.UpdateHealthTanks(energyTanks);
    }

    public void UpdateEnergyBar()
    {
        PlayerWeapon weapon = FindObjectOfType<PlayerWeapon>();
        if (weapon)
        {
            weapon.Energy = 1;
            UIManager.Instance.UpdateEnergyBar(weapon.Energy);
        }
    }

    public void UpdateHealthBar()
    {
        UIManager.Instance.UpdateHealthBar(10);
        playerHealth.CurrentHealth = playerHealth.MaxHealth;
    }
}
