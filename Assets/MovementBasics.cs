using System.Collections;
using UnityEngine;

public class MovementBasics : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 2.5f;
    public float gridMin = -2.5f;
    public float gridMax = 2.5f;
    public float dashDuration = 0.2f;

    [Header("Visuals & Audio")]
    public bool wantAudio = true;

    // Status Pergerakan
    private float currentX;
    private float currentY;
    private bool isDashing = false;

    // VARIABEL KUNCI: Matikan ini dari script lain (Collector/Slip) saat player mati
    public bool canMove = true;

    void Start()
    {
        // Mengambil posisi awal saat game dimulai agar tidak tiba-tiba teleport ke 0,0
        currentX = transform.position.x;
        currentY = transform.position.y;
    }

    void Update()
    {
        // Jika tidak boleh bergerak (mati) atau sedang dalam animasi dash, abaikan input
        if (!canMove || isDashing) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            AttemptDash(0f, moveDistance, 1);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            AttemptDash(0f, -moveDistance, 2);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            AttemptDash(-moveDistance, 0f, 3);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            AttemptDash(moveDistance, 0f, 4);
    }

    private void AttemptDash(float deltaX, float deltaY, int direction)
    {
        float newX = currentX + deltaX;
        float newY = currentY + deltaY;

        // Cek apakah target tujuan masih di dalam batas Grid
        if (newX >= gridMin && newX <= gridMax && newY >= gridMin && newY <= gridMax)
        {
            StartCoroutine(DashRoutine(newX, newY, deltaX));

            if (AudioManager.AudioManagerInstance != null && wantAudio)
            {
                AudioManager.AudioManagerInstance.Play(SFX.ChangeGrid);
            }
        }
        else
        {
            // Mentok tembok / pinggir grid
            Debug.Log("Mentok grid: Resetting to idle");
        }
    }

    private IEnumerator DashRoutine(float targetX, float targetY, float deltaX)
    {
        isDashing = true;

        Vector3 startPos = transform.position;
        // Z tetap menggunakan yang asli agar tidak error secara visual (depth)
        Vector3 targetPos = new Vector3(targetX, targetY, transform.position.z);

        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            while (Time.timeScale == 0) yield return null; // Pause game handling

            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / dashDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Pastikan posisi akhirnya pas (tidak meleset karena desimal Lerp)
        transform.position = targetPos;
        currentX = targetX;
        currentY = targetY;

        isDashing = false;


    }

    /// <summary>
    /// Panggil fungsi ini dari script Collector atau SlipMechanic saat player mati.
    /// Ini akan langsung mengunci pergerakan dan membatalkan dash yang sedang berjalan.
    /// </summary>
    public void DisableMovement()
    {
        canMove = false;
        StopAllCoroutines();
    }
}