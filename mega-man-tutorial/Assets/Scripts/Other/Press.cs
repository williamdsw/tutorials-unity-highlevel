using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    // FIELDS

    // config
    [SerializeField] private Vector2 interval = new Vector2 (0.5f, 2f);

    // cached
    private Animator animator;

    // MONOBEHAVIOUR FUNCTIONS

    private void Awake ()
    {
        animator = this.GetComponent<Animator>();
        animator.enabled = false;
    }

    private IEnumerator Start () 
    {
        yield return new WaitForSeconds (Random.Range (interval.x, interval.y));
        animator.enabled = true;
    }
}
