using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Perhatikan: PowerUp adalah ANAK dari FallingObstacle
// Dia otomatis mewarisi 'speed', 'death', dan fungsi 'Update' (Jatuh)
public class PowerUp : FallingObstacle
{
    // Kita tidak perlu menulis void Update() lagi.
    // Unity otomatis meminjam 'Update' milik bapaknya (FallingObstacle)
    // Jadi dia otomatis jatuh ke bawah.

    // Kita hanya perlu menambahkan fitur baru: "Bisa Diambil"
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah yang menabrak adalah Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("PowerUp Collected: " + gameObject.name);

            // --- DI SINI LOGIKA EFEK POWER UP NANTI ---
            // (Misal: Tambah nyawa, Shield, Slow motion)

            // Hancurkan PowerUp setelah diambil
            Destroy(gameObject);
        }
    }
}