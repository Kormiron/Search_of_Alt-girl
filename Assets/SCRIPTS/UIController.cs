using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider PlayerHealthSlider;
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private Slider PlayerExperienceSlider;
    [SerializeField] private TMP_Text ExperienceText;
    [SerializeField] private TMP_Text TimerText;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject LevelUpPanel;

    public LevelUpButton[] LevelUpButtons;


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

    // update HP Bar
    public void UpdateHelthSlider()
    {
        PlayerHealthSlider.maxValue = Player.Instance.PlayerMaxHealth;
        PlayerHealthSlider.value = Player.Instance.PlayerHealth;

        HealthText.text = PlayerHealthSlider.value+"/"+ PlayerHealthSlider.maxValue;
    }

    public void UpdateExperienceSlider()
    {
        PlayerExperienceSlider.maxValue = Player.Instance.PlayerLevels[Player.Instance.currentLevel];
        PlayerExperienceSlider.value = Player.Instance.Experience;

        ExperienceText.text = PlayerExperienceSlider.value + "/" + PlayerExperienceSlider.maxValue;
    }


    // Creates game timer
    public void UpdateTimer(float Time)
    {
        float min = Mathf.FloorToInt(Time / 60f);
        float sec = Mathf.FloorToInt(Time % 60f);

        TimerText.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    public void LevelUpPanelOpen()
    {
        LevelUpPanel.SetActive(true);
    }

    public void LevelUpPanelClose()
    {
        LevelUpPanel.SetActive(false);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetState(GameManager.GameState.Gameplay);
        }
    }
}
