using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keirengan : MonoBehaviour
{
    [Header("Component")]
    public SpriteRenderer irengSprite;

    [Header("Time Settings")]
    [Tooltip("Lama dia diam di kondisi TERANG (Transparan)")]
    public float intervalTerang = 10f;

    [Tooltip("Berapa lama dia diam di kondisi GELAP (Hitam)")]
    public float durasiGelap = 10f;

    [Tooltip("Berapa lama proses berubahnya?")]
    public float durasiTransformasi = 5f;

    [Header("Darkness Setting")]
    [Range(0f, 1f)]
    [Tooltip("Tingkat kegelapan maksimal (0-1)")]
    public float tingkatKeirengan = 1f;

    void Start()
    {
        if (irengSprite == null)    //Supaya tidak crash kalau lupa assign di sprite inspector
            irengSprite = GetComponent<SpriteRenderer>();

        SetAlpha(0f);   //Sprite mulai dari terang (sebenarnya dari transparan)
        StartCoroutine(IrengTerangCoroutine());
    }

    IEnumerator IrengTerangCoroutine()
    {
        while (true)
        {
            // Awalnya terang/transparan
            yield return new WaitForSeconds(intervalTerang);


            // Menjadi gelap di interval waktu tertentu
            float timer = 0f;
            while (timer < durasiTransformasi)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, tingkatKeirengan, timer / durasiTransformasi);
                SetAlpha(alpha);
                yield return null;
            }
            SetAlpha(tingkatKeirengan);


            // Diam di gelap
            yield return new WaitForSeconds(durasiGelap);


            // Menjadi cerah kembali
            timer = 0f;
            while (timer < durasiTransformasi)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(tingkatKeirengan, 0f, timer / durasiTransformasi);
                SetAlpha(alpha);
                yield return null;
            }
            SetAlpha(0f); 
        }
    }

    void SetAlpha(float alpha)
    {
        Color color = irengSprite.color;
        color.a = alpha;
        irengSprite.color = color;
    }
}