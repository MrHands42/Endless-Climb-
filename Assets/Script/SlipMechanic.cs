using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipMechanic : MonoBehaviour
{
    [Header("SlipTime")]
    [Tooltip("Batas waktu diam sebelum mati (detik)")]
    public float batasWaktuDiam = 7f;

    [Header("Tolerance")]
    [Tooltip("Seberapa sensitif deteksinya?")]
    public float toleransiGerak = 0.0000001f;

    // Variabel Logika
    private float timerDiam = 0f;
    private bool isDead = false;
    private Vector3 posisiTerakhir; // Untuk menyimpan koordinat frame sebelumnya

    void Start()
    {
        // Catat posisi awal
        posisiTerakhir = transform.position;
    }

    void Update()
    {
        // Jika sudah mati, stop semua supaya ga error
        if (isDead) return;

        float jarakGerak = Vector3.Distance(transform.position, posisiTerakhir);

        // Cek jaraknya seberapa jauh dari toleransi (kalau perubahan jaraknya lebih besar dari toleransi, berarti dia bergerak)
        // Kode ini sebenarnya buat mencegah error gajelas aja
        if (jarakGerak <= toleransiGerak)   // Kalau player diam
        {
            timerDiam += Time.deltaTime;
            
            if (timerDiam >= batasWaktuDiam)    // Kalau diamnya udah kelewatan batasWaktuDiam
            {
                Debug.Log("Kamu tidak bergerak terlalu lama.");
                RopeSlip();
            }
        }
        else // Kalau player bergerak atau tidak diam
        {
            timerDiam = 0f;
        }

        // Update posisi terakhir untuk pengecekan di frame berikutnya
        posisiTerakhir = transform.position;
    }

    // Fungsi ropeslipnya:
    public void RopeSlip()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Rope Slip :(");

        // Hentikan waktu di game, nanti
        // GANTI KODE INI!!!!!!
        Time.timeScale = 0f;
    }
}