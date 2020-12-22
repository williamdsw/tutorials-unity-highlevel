using UnityEngine;

public class EnemyHealth : Damageable
{
    public override void Death()
    {
        Destroy(this.gameObject);
    }

    protected override void Start()
    {
        base.Start();
    }
}
