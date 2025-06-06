using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null;

    bool isPaused;

    public bool GetIsPaused() { return isPaused; }
    
    public void ResumeButton()
    {
        isPaused = false;
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    private void Start()
    {
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
        }

        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
    }
}
