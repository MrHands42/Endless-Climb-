using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text pointText;
    public Text highScoreText;
    public Text GameOverScore;
    
    private int point = 0;  // Skor akhir (baseScore * timer)
    private int highScore = 0;
    
    // Variabel untuk skor default
    private int baseScore = 1;  // Skor dasar tetap (sesuaikan jika perlu)
    private int timer = 0;  // Timer dalam detik penuh (integer)
    private float elapsedTime = 0f;  // Untuk tracking waktu delta

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
        point = 0;
        
        // Update UI awal
        UpdateUI();
    }

    void Update()
    {
        // Increment elapsedTime dengan deltaTime
        elapsedTime += Time.deltaTime;
        
        // Jika sudah 1 detik penuh, increment timer dan reset elapsedTime
        if (elapsedTime >= 1f)
        {
            timer += 1;  // Tambah 1 detik
            elapsedTime = 0f;  // Reset
            
            // Hitung skor akhir (baseScore * timer) setiap detik
            point = baseScore * timer;
            
            // Periksa dan update high score
            if (point > highScore)
            {
                highScore = point;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
            }
            
            // Update UI setiap detik
            UpdateUI();
        }
    }

    // Method fallback untuk menambah skor manual (jika diperlukan)
    public void AddPoint()
    {
        point += 1;
        UpdateUI();
    }

    // Method untuk reset (misalnya saat game over atau restart)
    public void ResetScore()
    {
        point = 0;
        timer = 0;
        elapsedTime = 0f;
        UpdateUI();
    }

    // Method untuk update UI
    private void UpdateUI()
    {
        if (pointText != null)
        {
            pointText.text = point.ToString();
        }
        if (GameOverScore != null)  // Pisahkan if untuk GameOverScore agar lebih aman
        {
            GameOverScore.text = point.ToString();
        }
        if (highScoreText != null)
        {
            highScoreText.text = "HIGHSCORE: " + highScore.ToString();
        }
    }
}