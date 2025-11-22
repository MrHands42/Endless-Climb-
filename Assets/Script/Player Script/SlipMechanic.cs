using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipMechanic : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Batas waktu diam sebelum mati (detik)")]
    public float batasWaktuDiam = 7f;

    [Tooltip("Seberapa sensitif deteksinya?")]
    public float toleransiGerak = 0.001f;
    private float timerDiam = 0f;
    private bool isDead = false;
    private Vector3 posisiTerakhir;

    void Start()
    {
        posisiTerakhir = transform.position;
    }

    void Update()
    {
        if (isDead) return;

        float jarakGerak = Vector3.Distance(transform.position, posisiTerakhir);

        if (jarakGerak <= toleransiGerak)
        {
            timerDiam += Time.deltaTime;

            if (timerDiam >= batasWaktuDiam)
            {
                Debug.Log("Terlalu lama diam! Tali licin.");
                RopeSlip();
            }
        }
        else
        {
            timerDiam = 0f;
        }

        posisiTerakhir = transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RopeSlip();
        }
    }

    public void RopeSlip()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Penyebab Kematian: Rope Slip");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            Time.timeScale = 0f;    
        }
    }

}