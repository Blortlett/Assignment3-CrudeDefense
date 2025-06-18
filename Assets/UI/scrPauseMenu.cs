using UnityEngine;
using UnityEngine.SceneManagement;

public class scrPauseMenu : MonoBehaviour
{
    private bool mIsPaused;
    private CanvasGroup mCanvasGroup;

    [SerializeField] private AudioSource mMusicAudio;
    [SerializeField] private AudioSource[] mSFXAudioSources;

    private void Awake()
    {
        mCanvasGroup = GetComponent<CanvasGroup>();
        mCanvasGroup.alpha = 0f;
        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        // Pause game on player presses escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mIsPaused = true;
            mCanvasGroup.alpha = 1f; // Show pause menu
            Time.timeScale = 0f; // Pause game
            UnityEngine.Cursor.visible = true;
        }
    }

    public void ResumeGame()
    {
        if (mCanvasGroup.alpha < 1f) return;
        mIsPaused = false;
        mCanvasGroup.alpha = 0f; // Hide pause menu
        Time.timeScale = 1f; // Resume game
        UnityEngine.Cursor.visible = false;
    }

    public void QuitToMenu()
    {
        if (mCanvasGroup.alpha < 1f) return;
        Time.timeScale = 1f; // Reset time scale to ensure main menu works correctly
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleMuteMusic()
    {
        if (mCanvasGroup.alpha < 1f) return;
    }
    
    public void ToggleMuteSFX()
    {
        if (mCanvasGroup.alpha < 1f) return;
    }

}
