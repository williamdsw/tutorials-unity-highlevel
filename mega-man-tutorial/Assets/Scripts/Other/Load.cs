using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    [SerializeField] private AudioClip loadSong;
    [SerializeField] private GameObject[] bosses;

    private IEnumerator Start()
    {
        bosses[GameManager.Instance.Boss].SetActive(true);
        MusicController.Instance.PlaySong(loadSong);
        SceneController.Instance.StartScene();
        yield return new WaitForSeconds(8f);
        SceneController.Instance.LoadScene(GameManager.Instance.LevelSelected);
    }
}
