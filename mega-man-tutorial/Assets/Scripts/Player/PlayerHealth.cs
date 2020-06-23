using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damageable
{
    // FIELDS

    private int defaultLayer;

    // MONOBEHAVIOUR FUNCTIONS

    protected void Start () 
    {
        base.Start ();
        defaultLayer = gameObject.layer;
    }

    // FUNCTIONS

    public override void Death ()
    {
        Debug.Log ("Morreu");
    }

    public void SetInvencible (bool state)
    {
        int currentLayer = (state ? LayerMask.NameToLayer ("Invencible") : defaultLayer);
        gameObject.layer = currentLayer;
    }
}
