using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
public static ScoreManager instance;

    public Text scoreText;
    public Text highscoreText;

    private int score = 0;
    private int highscore = 0;

    private bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = score + " POINTS";
        highscoreText.text = "HIGHSCORE: " + highscore;
    }

    public void AddPoint(int value = 1)
    {
        if (!isPaused)
        {
            score += value;
            UpdateScoreUI();

            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("highscore", highscore);
                PlayerPrefs.Save();
            }
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    public void SetPause(bool pauseState)
    {
        isPaused = pauseState;
    }
}
