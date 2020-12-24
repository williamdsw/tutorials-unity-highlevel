using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class IntroMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private IntroText[] texts;

    private AudioSource audioSource;
    private bool hasStarted = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Start()
    {
        Cursor.visible = false;
        SceneController.Instance.StartScene();
        yield return new WaitForSeconds(1f);

        foreach (IntroText current in texts)
        {
            text.text = current.Text;
            yield return StartCoroutine(FadingText());
            yield return new WaitForSeconds(current.Time);
            yield return StartCoroutine(FadingOutText());
        }
    }

    private void Update()
    {
        if (hasStarted) return;
        if (Keyboard.current.anyKey.isPressed)
        {
            hasStarted = true;
            MusicController.Instance.FadeSong();
            SceneController.Instance.LoadScene("Menu");
            audioSource.Play();
        }
    }


    private IEnumerator FadingText()
    {
        Color color = text.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime;
            text.color = color;
            yield return null;
        }
    }

    private IEnumerator FadingOutText()
    {
        Color color = text.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            text.color = color;
            yield return null;
        }
    }
}
