using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] private Vector2 speed;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime);
    }

}
