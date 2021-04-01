using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI energyText;
        [SerializeField] private TextMeshProUGUI lifeText;
        [SerializeField] private Image energyBar;
        [SerializeField] private Image healthBar;
        [SerializeField] private Image bossHealthBar;
        [SerializeField] private TextMeshProUGUI[] weaponsTexts;
        [SerializeField] private GameObject weaponPanel;
        [SerializeField] private Transform cursor;

        private int cursorIndex;
        private bool isPaused = false;

        private static UIManager instance;

        public static UIManager Instance { get => instance; private set => instance = value; }
        public bool IsPaused { get => isPaused; set => isPaused = value; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            for (int index = 0; index < GameManager.Instance.Weapons.Count; index++)
            {
                weaponsTexts[index].text = GameManager.Instance.Weapons[index].Name;
            }
        }

        public void OnEnableMenu(InputValue value)
        {
            Pause();
        }

        private void Pause()
        {
            IsPaused = !IsPaused;
            weaponPanel.SetActive(!weaponPanel.activeSelf);

            if (IsPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                if (PlayerWeapon.Instance.IsTimeStopped)
                {
                    Time.timeScale = 0.25f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
            }
        }

        public void OnSelectWeapon(InputValue value)
        {
            if (!weaponPanel.activeSelf) return;
            PlayerWeapon.Instance.SelectWeapon(GameManager.Instance.Weapons[cursorIndex]);
            Pause();
        }

        public void OnUp(InputValue value)
        {
            if (!weaponPanel.activeSelf || cursorIndex <= 0) return;
            cursorIndex--;
            cursor.localPosition = new Vector2(cursor.localPosition.x, cursor.localPosition.y + 45);
        }

        public void OnDown(InputValue value)
        {
            if (!weaponPanel.activeSelf || cursorIndex >= GameManager.Instance.Weapons.Count - 1) return;
            cursorIndex++;
            cursor.localPosition = new Vector2(cursor.localPosition.x, cursor.localPosition.y - 45);
        }

        public void UpdateEnergyBar(float energy)
        {
            energyBar.fillAmount = energy;
        }

        public void UpdateHealthBar(float health)
        {
            healthBar.fillAmount = (health / 10);
        }

        public void UpdateBossHealthBar(float health)
        {
            bossHealthBar.fillAmount = (health / 30);
        }

        public void UpdateHealthTanks(int health)
        {
            lifeText.text = string.Concat("x", health);
        }

        public void UpdateEnergyTanks(int energy)
        {
            energyText.text = string.Concat("x", energy);
        }
    }
}