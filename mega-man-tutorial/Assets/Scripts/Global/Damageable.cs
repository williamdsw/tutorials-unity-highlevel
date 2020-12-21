using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float invencibleTime;
    private Color defaultColor;
    private float timeToWait = 0.05f;

    protected int currentHealth;
    private bool canTakeDamage = true;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        defaultColor = spriteRenderer.color;
    }

    public UnityEvent OnDamage;
    public UnityEvent OnFinishDamage;
    public UnityEvent OnDeath;

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage) return;

        canTakeDamage = false;
        currentHealth -= amount;
        OnDamage.Invoke();
        StartCoroutine(TakingDamage());

        if (currentHealth <= 0)
        {
            OnDeath.Invoke();
            Death();
        }
    }

    private IEnumerator TakingDamage()
    {
        float timer = 0;
        while (timer < invencibleTime)
        {
            spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(timeToWait);
            spriteRenderer.color = defaultColor;
            yield return new WaitForSeconds(timeToWait);
            timer += 0.1f;
        }

        spriteRenderer.color = defaultColor;
        canTakeDamage = true;
        OnFinishDamage.Invoke();
    }

    public abstract void Death();
}
