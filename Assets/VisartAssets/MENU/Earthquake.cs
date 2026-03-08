using UnityEngine;
using UnityEngine.UI;

public class ImageShakeController : MonoBehaviour
{
    [Header("TARGETS")]
    public Image[] targetsToShake; // Masukkan Image yang mau digetarkan di sini

    [Header("SHAKE SETTINGS")]
    public float baseIntensity = 5f; // Kekuatan dasar getaran
    public float shakeSpeed = 10f;   // Kecepatan getaran

    [Header("AUTO START (UNTUK TESTING)")]
    public bool autoStartShake = false; // Set true untuk auto-start saat game mulai
    public float autoStartIntensity = 5f;
    public float autoStartDuration = 2f;

    private Vector2[] originalPositions;
    private bool isShaking = false;
    private float shakeTimer = 0f;
    private float currentDuration = 0f;

    void Start()
    {
        // Simpan posisi awal semua target
        originalPositions = new Vector2[targetsToShake.Length];
        int validTargets = 0;

        for (int i = 0; i < targetsToShake.Length; i++)
        {
            if (targetsToShake[i] != null)
            {
                RectTransform rt = targetsToShake[i].GetComponent<RectTransform>();
                if (rt != null)
                {
                    originalPositions[i] = rt.anchoredPosition;
                    validTargets++;
                }
                else
                {
                    Debug.LogWarning("ImageShakeController: Image " + i + " tidak punya RectTransform!");
                }
            }
        }

        if (validTargets == 0)
        {
            Debug.LogError("ImageShakeController: Tidak ada target valid! Pastikan targetsToShake terisi.");
        }
        else
        {
            Debug.Log("ImageShakeController: " + validTargets + " target valid ditemukan.");
        }

        // Auto-start untuk testing
        if (autoStartShake)
        {
            StartShake(autoStartIntensity, autoStartDuration);
        }
    }

    void Update()
    {
        if (!isShaking) return;

        // Update timer
        shakeTimer += Time.deltaTime;

        // Cek apakah durasi habis (jika duration > 0)
        if (currentDuration > 0 && shakeTimer >= currentDuration)
        {
            StopShake();
            return;
        }

        // Hitung kekuatan getaran
        float currentIntensity = baseIntensity;

        // Efek gempa acak
        float offsetX = Random.Range(-1f, 1f) * currentIntensity;
        float offsetY = Random.Range(-1f, 1f) * currentIntensity;

        // Shake semua target
        for (int i = 0; i < targetsToShake.Length; i++)
        {
            if (targetsToShake[i] != null)
            {
                RectTransform rt = targetsToShake[i].GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = originalPositions[i] + new Vector2(offsetX, offsetY);
                }
            }
        }
    }

    // Method untuk memulai getaran
    public void StartShake(float intensity, float duration)
    {
        isShaking = true;
        shakeTimer = 0f;
        currentDuration = duration;
        baseIntensity = intensity;
        Debug.Log("ImageShakeController: Shake dimulai! Intensitas: " + intensity + ", Durasi: " + duration);
    }

    // Method untuk menghentikan getaran
    public void StopShake()
    {
        isShaking = false;
        shakeTimer = 0f;
        for (int i = 0; i < targetsToShake.Length; i++)
        {
            if (targetsToShake[i] != null)
            {
                RectTransform rt = targetsToShake[i].GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = originalPositions[i];
                }
            }
        }
        Debug.Log("ImageShakeController: Shake dihentikan.");
    }
}