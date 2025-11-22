using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text pointText;
    public Text highScoreText;
    
    private int point = 0;  
    private int highScore = 0;
    
    private float maxHeight = 0f;  

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // Muat high score dari PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        // Update UI awal
        UpdateUI();
    }

    public void AddVerticalScore(float deltaY)
    {
        if (deltaY > 0)
        {
            maxHeight += deltaY;
            
            point = Mathf.FloorToInt(maxHeight * 10);
            
            if (point > highScore)
            {
                highScore = point;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
            }
            
            UpdateUI();
        }
    }

    public void AddPoint()
    {
        point += 1;
        UpdateUI();
    }

    public void ResetScore()
    {
        point = 0;
        maxHeight = 0f;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (pointText != null)
        {
            pointText.text = point.ToString() + " POINTS";
        }
        if (highScoreText != null)
        {
            highScoreText.text = "HIGHSCORE: " + highScore.ToString();
        }
    }
}
