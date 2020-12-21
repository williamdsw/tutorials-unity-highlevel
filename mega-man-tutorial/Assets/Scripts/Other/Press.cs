using System.Collections;
using UnityEngine;

public class Press : MonoBehaviour
{
    [SerializeField] private Vector2 interval = new Vector2(0.5f, 2f);
    private Animator animator;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        animator.enabled = false;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(interval.x, interval.y));
        animator.enabled = true;
    }
}
