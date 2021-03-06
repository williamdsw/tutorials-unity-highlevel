﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeatEmUpTutorial
{
    public class Menu : MonoBehaviour
    {
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                LoadScene();
            }
        }

        private void LoadScene()
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentIndex + 1);
        }
    }
}