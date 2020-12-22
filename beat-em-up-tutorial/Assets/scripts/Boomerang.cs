using System.Collections;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] private int bossDirection = 1;
    private Rigidbody rigidBody;

    public int BossDirection { get => bossDirection; set => bossDirection = value; }

    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector3(6 * BossDirection, 0, 2 * BossDirection);
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(2f);
        BossDirection *= -1;
    }
}