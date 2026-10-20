using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int currentScore = 0;  
    public int highScore = 0;      

    public TMP_Text scoreText;     
    public TMP_Text highScoreText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

   
    public void AddScore(int amount)
    {
        currentScore += amount;

        
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore); 
            PlayerPrefs.Save();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;

        if (highScoreText != null)
            highScoreText.text = "Best: " + highScore;
    }
}

