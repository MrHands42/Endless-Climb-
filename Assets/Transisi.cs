//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class Transisi : MonoBehaviour
//{
//    public static Transisi instance;

//    [Header("Animator Transisi")]
//    public Animator GameStart1;
//    public Animator CrossFade;

//    [Header("Pengaturan Waktu")]
//    public float transitionTime = 1.5f;

//    private void Awake()
//    {
//        // Setup instance agar skrip ini bisa dipanggil dari mana saja
//        if (instance == null)
//        {
//            instance = this;
//        }
//    }

//    private void Start()
//    {
//        // Jangan langsung tembak trigger, gunakan Coroutine untuk menunggu sesaat
//        StartCoroutine(TriggerTerangDelay());
//    }

//    private IEnumerator TriggerTerangDelay()
//    {
//        // WAJIB: Tunggu 1 frame agar Animator benar-benar siap 100%
//        yield return null;

//        // Skenario 1 & 2: Scene selesai di-load, jalankan animasi terang
//        if (CrossFade != null)
//        {
//            CrossFade.SetTrigger("Terangkan");
//            Debug.Log("Scene Dimulai: CrossFade Terangkan berjalan.");
//        }
//    }

//    // ==========================================================
//    // FUNGSI-FUNGSI PANGGILAN TRANISI
//    // ==========================================================

//    // 1. Panggil ini di Tombol Play / Mulai dari Main Menu
//    public void KeLevel(string namaLevelTujuan)
//    {
//        if (namaLevelTujuan == null || namaLevelTujuan.Trim() == "")
//        {
//            namaLevelTujuan = "GameScene";
//        }
//        StartCoroutine(ProsesLoadLevel(GameStart1, "Mulai", namaLevelTujuan));
//    }

//    // 2. Panggil ini saat Player Mati atau pencet Restart
//    public void RestartScene()
//    {
//        // Mengambil nama scene yang sedang aktif saat ini
//        string sceneSekarang = SceneManager.GetActiveScene().name;
//        StartCoroutine(ProsesLoadLevel(CrossFade, "Gelapkan", sceneSekarang));
//    }

//    // 3. Panggil ini saat Player kembali ke Main Menu dari Level
//    public void KeMainMenu()
//    {
//        // Pastikan nama scene menu-mu di Build Settings benar-benar "MainMenu"
//        StartCoroutine(ProsesLoadLevel(GameStart1, "Akhiri", "MainMenu"));
//    }

//    // ==========================================================
//    // MESIN UTAMA (COROUTINE)
//    // ==========================================================

//    // Perhatikan: Parameter pertama sekarang adalah 'Animator', bukan 'string'
//    private IEnumerator ProsesLoadLevel(Animator animatorTransisi, string namaTrigger, string sceneName)
//    {
//        // 1. Jalankan animasi (misal: layar menutup)
//        if (animatorTransisi != null)
//        {
//            animatorTransisi.SetTrigger(namaTrigger);
//        }
//        else
//        {
//            Debug.LogWarning("Animator tidak ditemukan! Pastikan sudah ditarik di Inspector.");
//        }

//        // 2. GUNAKAN REALTIME (Mengabaikan Time.timeScale = 0)
//        yield return new WaitForSecondsRealtime(transitionTime);

//        // 3. WAJIB: Normalkan waktu kembali sebelum scene baru dimuat!
//        Time.timeScale = 1f;

//        // 4. Load scene baru setelah layar tertutup
//        Debug.Log("Loading scene: " + sceneName);
//        SceneManager.LoadScene(sceneName);
//    }
//}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transisi : MonoBehaviour
{
    public Animator Transition;

    public float transitiononTime = 1f;

    public static Transisi instance;
    public void Start()
    {
        instance = this;
        Debug.Log("Transisi instance set: " + instance);
    }

    public void LoadCurrentLevel()
    {
        StartCoroutine(LooadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public IEnumerator LooadLevel(int LevelIndex)
    {
        //// Trigger SFX close sebelum animasi
        //if (AudioManager.AudioManagerInstance != null)
        //{
        //    AudioManager.AudioManagerInstance.Play(SFX.Gate_Close_Transition);
        //}

        if (Transition != null)
        {
            Transition.SetTrigger("Terangkan");
        }
        else
        {
            Debug.LogWarning("Transition Animator not assigned!");
        }

        yield return new WaitForSeconds(transitiononTime);

        if (LevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(LevelIndex);
            Debug.Log("Loading scene by index: " + LevelIndex);
        }
        else
        {
            Debug.LogError("Scene index " + LevelIndex + " not found in Build Settings!");
        }
    }

    public IEnumerator LooadLevel(string sceneName)
    {
        // Trigger SFX close sebelum animasi
        //if (AudioManager.AudioManagerInstance != null)
        //{
        //    AudioManager.AudioManagerInstance.Play(SFX.Gate_Close_Transition);
        //}

        if (Transition != null)
        {
            Transition.SetTrigger("Start");
        }
        else
        {
            Debug.LogWarning("Transition Animator not assigned!");
        }

        yield return new WaitForSeconds(transitiononTime);

        // Tambah: Load scene setelah transisi
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading scene by name: " + sceneName);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LooadLevel("MainMenu"));
    }
}