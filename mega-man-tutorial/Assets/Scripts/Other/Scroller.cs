using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] private Vector2 speed;

    public Vector2 Speed { get => speed; private set => speed = value; }

    private void Update()
    {
        transform.Translate(Speed * Time.deltaTime);
    }
}
