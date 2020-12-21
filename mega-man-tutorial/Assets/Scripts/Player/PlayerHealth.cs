using UnityEngine;

public class PlayerHealth : Damageable
{
    private int defaultLayer;

    protected override void Start()
    {
        base.Start();
        defaultLayer = gameObject.layer;
    }

    public override void Death()
    {
        Debug.Log("Morreu");
    }

    public void SetInvencible(bool state)
    {
        int currentLayer = (state ? LayerMask.NameToLayer("Invencible") : defaultLayer);
        gameObject.layer = currentLayer;
    }
}
