using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipMechanic : MonoBehaviour
{
    [Header("Settings")]
    public float batasWaktuDiam = 7f;
    public float toleransiGerak = 0.001f;

    [Header("Vibration Settings")]
    public Transform playerBodyVisual;
    
    public float kekuatanGetar = 0.5f;
    private float timerDiam = 0f;
    private bool isDead = false;
    private Vector3 posisiTerakhir;
    private Vector3 posisiAsliBody;
    public float mulaiGetarDetik;

    void Start()
    {
        if (mulaiGetarDetik == 0)
        {
            mulaiGetarDetik = batasWaktuDiam * (2f / 3f);
        }


        posisiTerakhir = transform.position;

        if (playerBodyVisual != null)
        {
            posisiAsliBody = playerBodyVisual.localPosition;
        }
    }

    void Update()
    {
        if (isDead) return;
        float jarakGerak = Vector3.Distance(transform.position, posisiTerakhir);

        if (jarakGerak <= toleransiGerak)
        {
            // Kondisi diam
            timerDiam += Time.deltaTime;

            if (timerDiam > mulaiGetarDetik)
            {
                GetarkanPlayer();
            }

            // RopeSlip jika melewati batas waktu diam
            if (timerDiam >= batasWaktuDiam)
            {
                RopeSlip();
            }
        }
        else
        {
            // Kondisi bergerak
            timerDiam = 0f;
            ResetPosisiPlayer();
        }

        posisiTerakhir = transform.position;

        if (Input.GetKeyDown(KeyCode.Space)) RopeSlip();
    }

    void GetarkanPlayer()
    {
        if (playerBodyVisual != null)
        {
            float intensity = kekuatanGetar * (timerDiam / batasWaktuDiam);
            Vector3 randomShake = (Vector3)Random.insideUnitCircle * intensity;
            playerBodyVisual.localPosition = posisiAsliBody + randomShake;
        }
    }

    void ResetPosisiPlayer()
    {
        if (playerBodyVisual != null)
        {
            playerBodyVisual.localPosition = posisiAsliBody;
        }
    }

    public void RopeSlip()
    {
        if (isDead) return;
        isDead = true;

        ResetPosisiPlayer();
        Debug.Log("Mati: Rope Slip");

        if (GameManager.Instance != null)
            GameManager.Instance.GameOver();
        else
            Time.timeScale = 0f;
    }
}