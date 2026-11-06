using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float GameTime;
    public bool GameActive;
    public GameObject mainPanel;
    public GameObject optionsPanel;

    public enum GameState
    {
        GameStart,
        Gameplay,
        Pause,
        LevelUp,
        GameOver
    }

    public GameState currentState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // contolling timer if game overed
    void Start()
    {
        
        SetState(GameState.Gameplay);
        if (mainPanel != null) mainPanel.SetActive(true);
        if (optionsPanel != null) optionsPanel.SetActive(false);
    }
    void Update()
    {
        if (currentState == GameState.Gameplay)
        {
            GameTime += Time.deltaTime;
            if (UIController.Instance != null)
            {
                UIController.Instance.UpdateTimer(GameTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (currentState == GameState.Gameplay)
            {
                SetState(GameState.Pause);
            }
            else if (currentState == GameState.Pause)
            {
                SetState(GameState.Gameplay);
            }
        }
    }

    public void SetState(GameState newState)
    {
        currentState = newState;

        if (currentState == GameState.Gameplay)
        {
            if (UIController.Instance != null)
            {
                UIController.Instance.PausePanel.SetActive(false);
                UIController.Instance.LevelUpPanel.SetActive(false);
            }
        }
        else if (currentState == GameState.Pause)
        {
            if (UIController.Instance != null)
            {
                UIController.Instance.PausePanel.SetActive(true);
            }
        }
        else if (currentState == GameState.LevelUp)
        {
            if (UIController.Instance != null)
            {
                UIController.Instance.LevelUpPanelOpen();
            }
        }
        else if (currentState == GameState.GameOver)
        {
            if (UIController.Instance != null)
            {
                UIController.Instance.GameOverPanel.SetActive(true);
            }
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene("GAME");
    }

    public void OpenOptions()
    {
        if (mainPanel != null) mainPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }
    public void CloseOptions()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (mainPanel != null) mainPanel.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        if (volume <= 0.001f)
        {
            AudioListener.volume = 0f;
        }
        else
        {
            AudioListener.volume = volume;
        }

        PlayerPrefs.SetFloat("GameVolume", volume);
    }

   
    public void GameOver()
    {
        SetState(GameState.GameOver);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
