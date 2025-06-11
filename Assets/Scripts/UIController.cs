using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;



public class UIController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject loadingPanel;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject storePanel;
    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    [Header("Levels")]
    public GameObject[] levels;
    private int currentLevelIndex = 0;

    [Header("Extra Panels")]
    public GameObject allLevelCompletePanel;

    [Header("Gameplay")]
    public GameObject gameplayPanel;

    private SettingsSource lastSettingsSource = SettingsSource.None;

    private enum SettingsSource
    {
        None,
        FromMainMenu,
        FromPause
    }

    void Start()
    {
        loadingPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        storePanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        levels[currentLevelIndex].SetActive(false);
    }

    public void ShowMainMenuAfterLoading()
    {
        loadingPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }


    public void StartLevel(int levelIndex)
    {
        foreach (GameObject lvl in levels)
            lvl.SetActive(false);

        if (levelIndex >= 0 && levelIndex < levels.Length)
        {
            levels[levelIndex].SetActive(true);
            currentLevelIndex = levelIndex;

            gameplayPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
            gameOverPanel.SetActive(false);
            levelCompletePanel.SetActive(false);
            allLevelCompletePanel?.SetActive(false);

            BallTracker.instance?.ResetStateAndRespawnBall();

            int totalBalls = GameObject.FindGameObjectsWithTag("Ball").Length;

            FindObjectOfType<GamePlayManager>()?.InitializeLevel(currentLevelIndex, totalBalls);
        }
    }




    public void OpenSettings()
    {
        settingsPanel.SetActive(true);

        if (gameplayPanel.activeSelf)
        {
            gameplayPanel.SetActive(false);
        }

        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void OpenSettingsFromMainMenu()
    {
        lastSettingsSource = SettingsSource.FromMainMenu;
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void OpenSettingsFromPause()
    {
        lastSettingsSource = SettingsSource.FromPause;
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void BackFromSettings()
    {
        settingsPanel.SetActive(false);

        if (lastSettingsSource == SettingsSource.FromMainMenu)
        {
            mainMenuPanel.SetActive(true);
        }
        else if (lastSettingsSource == SettingsSource.FromPause)
        {
            pausePanel.SetActive(true);
        }

        lastSettingsSource = SettingsSource.None;
    }

    public void OpenStore()
    {
        storePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        FindObjectOfType<GamePlayManager>()?.StopGameplay();
        Debug.Log("🔻 Back to Main Menu - Stopping Gameplay");

        settingsPanel.SetActive(false);
        storePanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }



    public void ShowGameOver()
    { 

        levels[currentLevelIndex].SetActive(false);
        pausePanel.SetActive(false);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }


    public void RetryLevel()
    {
        BallTracker.instance.ResetStateAndRespawnBall(); 
        StartLevel(currentLevelIndex);
        gameOverPanel.SetActive(false);
    }

    public void ShowLevelCompletePanel()
    {
        if (currentLevelIndex == levels.Length - 1)
        {

            gameplayPanel.SetActive(false);
            levelCompletePanel.SetActive(false);
            allLevelCompletePanel.SetActive(true); 
        }
        else
        {
            gameplayPanel.SetActive(false);
            levelCompletePanel.SetActive(true);
        }
    }

    public void LoadNextLevel()
    {
        int nextIndex = currentLevelIndex + 1;
        levelCompletePanel.SetActive(false);

        if (nextIndex < levels.Length)
        {
            StartLevel(nextIndex);
        }
        else
        {
            Debug.Log("All levels completed!");
            gameplayPanel.SetActive(false);
            allLevelCompletePanel.SetActive(true); 
        }
    }

    public void BackToMenuAfterCompletion()
    {
        allLevelCompletePanel.SetActive(false);
        BackToMainMenu();

     
    }


    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        gameplayPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        gameplayPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
