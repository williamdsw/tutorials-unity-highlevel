using System.Collections;
using UnityEngine;
using TMPro;

public class GetEquippedScene : MonoBehaviour
{
    [SerializeField] private AudioClip song;
    [SerializeField] private string playerName = "Metalbot";
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Equipment[] equipments;

    private IEnumerator Start()
    {
        int bossIndex = GameManager.Instance.Boss;

        if (MusicController.Instance)
        {
            MusicController.Instance.PlaySong(song);
        }

        if (SceneController.Instance)
        {
            SceneController.Instance.StartScene();
        }

        yield return new WaitForSeconds(1f);

        string[] words =
        {
            "Metalbot", "get equipped", "with"
        };

        for (int i = 0; i < words.Length; i++)
        {
            for (int j = 0; j < words[i].Length; j++)
            {
                text.text += words[i];
                yield return new WaitForSeconds(0.1f);
            }

            text.text += "\n";
        }

        playerAnimator.Play(equipments[bossIndex].Animation);
        yield return new WaitForSeconds(2f);
        playerAnimator.Play(equipments[bossIndex].WeaponName);
        string weapon = equipments[bossIndex].WeaponName;

        for (int i = 0; i < weapon.Length; i++)
        {
            text.text += weapon[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2f);
        SceneController.Instance.LoadScene("Menu");
    }
}
