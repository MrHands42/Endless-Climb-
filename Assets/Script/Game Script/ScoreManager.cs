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
    
    // Variabel untuk skor default dan scaling
    private float baseMultiplier = 10f;  // Default multiplier untuk skor dasar (sesuaikan jika perlu)
    private float timer = 0f;  // Timer global (waktu bertahan)
    private float totalVerticalDistance = 0f;  // Total jarak vertikal ke atas yang ditempuh
    private const float DistanceThreshold = 1000f;  // Ambang batas jarak untuk mulai scaling (misalnya 1000 meter/unit)
    private const float ScalingIncreasePerSecond = 0.01f;  // Kenaikan multiplier per detik setelah threshold

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
        // Jalankan timer terus
        timer += Time.deltaTime;
        
        // Jika total jarak > threshold, naikkan multiplier (scaling aktif tanpa flag)
        if (totalVerticalDistance >= DistanceThreshold)
        {
            baseMultiplier += ScalingIncreasePerSecond * Time.deltaTime;  // Naik 0.01 per detik
        }
        
        // Update skor akhir (baseScore * timer) secara real-time
        // Modifikasi: Tambahkan 1 agar baseScore minimal 1, sehingga score mulai otomatis dari awal
        int baseScore = 1 + Mathf.FloorToInt(totalVerticalDistance * baseMultiplier);  // "1" untuk mulai otomatis
        point = Mathf.FloorToInt(baseScore * timer);
        
        // Periksa dan update high score
        if (point > highScore)
        {
            highScore = point;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        
        // Update UI
        UpdateUI();
    }

    // Method untuk menambah skor berdasarkan jarak vertikal ke atas
    // Dipanggil dari PlayerMovement saat dash ke atas selesai
    public void AddVerticalScore(float deltaY)
    {
        if (deltaY > 0)
        {
            totalVerticalDistance += deltaY;  // Tambah total jarak vertikal
            // Skor akhir akan dihitung ulang di Update() berdasarkan baseMultiplier dan timer
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
        timer = 0f;
        totalVerticalDistance = 0f;
        baseMultiplier = 10f;  // Reset ke default
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
            highScoreText.text = "HIGHSCRE: " + highScore.ToString();
        }
    }
}
