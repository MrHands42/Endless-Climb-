using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Wajib dipanggil untuk mengakses komponen UI

public class EarthquakeUI : MonoBehaviour
{
    [Header("Pengaturan Gempa")]
    public float durasiGempa = 0.5f;     // Berapa lama gempanya terjadi (detik)
    public float kekuatanGempa = 15f;    // Seberapa kuat guncangannya (dalam Piksel)

    // Referensi komponen UI
    private RectTransform rectTransform;
    private Vector2 posisiAsli;
    private Coroutine getarCoroutine;

    [Header("Uji Coba (Tekan G di Keyboard)")]
    public bool aktifkanTombolTest = true;

    void Awake()
    {
        // Mengambil komponen RectTransform (Wajib untuk objek Canvas)
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("Skrip EarthquakeUI harus dipasang di objek Canvas (UI)!");
        }
    }

    void Start()
    {
        // Menyimpan posisi awal gambar saat game dimulai
        if (rectTransform != null)
        {
            posisiAsli = rectTransform.anchoredPosition;
        }
    }

    void Update()
    {
        // Hanya untuk testing di editor, tekan tombol G untuk memicu gempa
        if (aktifkanTombolTest && Input.GetKeyDown(KeyCode.G))
        {
            MulaiGempa();
        }
    }

    /// <summary>
    /// Panggil fungsi ini dari skrip lain (misal saat player nabrak obstacle atau kena damage)
    /// </summary>
    public void MulaiGempa()
    {
        // Jika sedang ada gempa yang berjalan, hentikan dulu agar tidak bentrok
        if (getarCoroutine != null)
        {
            StopCoroutine(getarCoroutine);
        }

        // Mulai guncangan baru
        getarCoroutine = StartCoroutine(RutinitasGempa());
    }

    private IEnumerator RutinitasGempa()
    {
        float waktuBerjalan = 0f;

        // Looping guncangan selama durasi belum habis
        while (waktuBerjalan < durasiGempa)
        {
            // Mencari titik acak dalam radius kekuatan gempa
            float acakX = posisiAsli.x + Random.Range(-kekuatanGempa, kekuatanGempa);
            float acakY = posisiAsli.y + Random.Range(-kekuatanGempa, kekuatanGempa);

            // Terapkan posisi baru ke gambar
            rectTransform.anchoredPosition = new Vector2(acakX, acakY);

            // Tambah waktu. Kita pakai unscaledDeltaTime agar gempa tetap jalan 
            // walau game sedang di-pause (Time.timeScale = 0)
            waktuBerjalan += Time.unscaledDeltaTime;

            // Tunggu frame berikutnya
            yield return null;
        }

        // GEMPA SELESAI: Wajib kembalikan gambar persis ke titik aslinya agar UI tidak miring/cacat
        rectTransform.anchoredPosition = posisiAsli;
    }
}