using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingPlatform : MonoBehaviour
{
    [SerializeField] private int currentDirection = 1;
    [SerializeField] private SpriteRenderer[] arrowsSpriteRenderers;
    [SerializeField] private Sprite[] arrowsSprites; // 0 - EMPTY, 1 - RIGHT, 2 - LEFT
    [SerializeField] private float impulseForce = 200;

    private List<Rigidbody2D> listRigidBodies = new List<Rigidbody2D>();

    public int CurrentDirection { get => currentDirection; set => currentDirection = value; }

    private void Start()
    {
        StartCoroutine(Rolling());
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody2D rb in listRigidBodies)
        {
            rb.AddForce(Vector2.right * currentDirection * impulseForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D rb = null;
        if (checkIfRigidbodyExists(other, out rb))
        {
            listRigidBodies.Add(rb);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Rigidbody2D rb = null;
        if (checkIfRigidbodyExists(other, out rb))
        {
            if (listRigidBodies.Contains(rb))
            {
                listRigidBodies.Remove(rb);
            }
        }
    }

    private bool checkIfRigidbodyExists(Collision2D other, out Rigidbody2D current)
    {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        current = rb;
        return rb != null;
    }

    private IEnumerator Rolling()
    {
        while (true)
        {
            if (currentDirection > 0)
            {
                iterateSpriteRendererArrayAndSetSprite(arrowsSpriteRenderers, arrowsSprites[0]);
                yield return new WaitForSeconds(0.1f);
                iterateSpriteRendererArrayAndSetSprite(arrowsSpriteRenderers, arrowsSprites[1]);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                iterateSpriteRendererArrayAndSetSprite(arrowsSpriteRenderers, arrowsSprites[0]);
                yield return new WaitForSeconds(0.1f);
                iterateSpriteRendererArrayAndSetSprite(arrowsSpriteRenderers, arrowsSprites[2]);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void iterateSpriteRendererArrayAndSetSprite(SpriteRenderer[] array, Sprite sprite)
    {
        foreach (SpriteRenderer renderer in array)
        {
            if (renderer && sprite)
            {
                renderer.sprite = sprite;
            }
        }
    }

    public void Clear()
    {
        listRigidBodies.Clear();
    }
}
