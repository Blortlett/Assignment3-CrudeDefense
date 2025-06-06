using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _resumeButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            _resumeButton.SetActive(true);
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _resumeButton.SetActive(false);
    }
}
