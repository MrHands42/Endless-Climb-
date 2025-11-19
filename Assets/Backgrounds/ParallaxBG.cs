using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBG : MonoBehaviour
{
    private float startPos;
    private float length;

    public float parallaxEffect = 5f;        // Float yang menentukan seberapa cepat latar belakang bergerak

    [HideInInspector]
    public bool isclone = false;

    void Start()
    {
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;  //Tinggi gambar

        if (isclone == false)
        {
            GameObject clone = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y + length, transform.position.z), Quaternion.identity);

            //Menandai bahwa objek yang diinstansiasi adalah clone
            clone.GetComponent<ParallaxBG>().isclone = true;
        }
        if (isclone== true)
        {
            startPos = startPos - length;
        }
    }

    // Update is called once per    frame
    void Update()
    {
        transform.Translate(Vector2.down * parallaxEffect * Time.deltaTime);
        if (transform.position.y < startPos - length)   //Jika posisi y background sudah melewati titik awal    
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + length * 2, transform.position.z);

        }
    }
}
