//using System.Collections;
//using UnityEngine;

//public class SlipMechanic : MonoBehaviour
//{
//    [Header("Settings")]
//    public float batasWaktuDiam = 7f;
//    public float toleransiGerak = 0.001f;

//    [Header("Vibration Settings")]
//    public Transform playerBodyVisual;
//    public float kekuatanGetar = 0.05f;
//    private float mulaiGetarDetik;
//    private float timerDiam = 0f;
//    private bool isDead = false;
//    private Vector3 posisiTerakhir;
//    private Vector3 posisiAsliBody;


//    public Animator animator;
//    // gw benci banget ama lu animator
//    void Start()
//    {
//        mulaiGetarDetik = batasWaktuDiam * (2f / 3f);
//        posisiTerakhir = transform.position;

//        if (playerBodyVisual != null)
//            posisiAsliBody = playerBodyVisual.localPosition;
//    }

//    void Update()
//    {
//        if (isDead) return;

//        float jarakGerak = Vector3.Distance(transform.position, posisiTerakhir);

//        if (jarakGerak <= toleransiGerak)
//        {
//            timerDiam += Time.deltaTime;

//            if (timerDiam > mulaiGetarDetik) GetarkanBody();
//            if (timerDiam >= batasWaktuDiam) RopeSlip();
//        }
//        else
//        {
//            timerDiam = 0f;
//            ResetPosisiBody();
//        }

//        posisiTerakhir = transform.position;
//    }

//    void GetarkanBody()
//    {
//        if (playerBodyVisual != null)
//        {
//            float intensity = kekuatanGetar * (timerDiam / batasWaktuDiam);
//            Vector3 randomShake = (Vector3)Random.insideUnitCircle * intensity;
//            playerBodyVisual.localPosition = posisiAsliBody + randomShake;
//        }
//    }

//    void ResetPosisiBody()
//    {
//        if (playerBodyVisual != null) playerBodyVisual.localPosition = posisiAsliBody;
//    }

//    public void RopeSlip()
//    {
//        Collector collector = GetComponent<Collector>();

//        if (isDead) return;
//        isDead = true;

//        ResetPosisiBody();
//        Debug.Log("Player Jatuh!");

    
//        if (animator != null) animator.SetInteger("Direction", 7);
//        //solusi nya taik banget dawg
//        if (GetComponent<PlayerMovement>() != null)
//            GetComponent<PlayerMovement>().enabled = false;

//        if (GetComponent<Collider2D>() != null)
//            GetComponent<Collider2D>().enabled = false;

//        Rigidbody2D rb = GetComponent<Rigidbody2D>();
//        if (rb != null)
//        {
//            rb.bodyType = RigidbodyType2D.Dynamic; 
//            rb.gravityScale = 4f; 
//            rb.velocity = Vector2.zero; 

//            rb.AddForce(Vector2.up * 12f, ForceMode2D.Impulse);
//        }

//        if (AudioManager.AudioManagerInstance != null)
//        {
//            AudioManager.AudioManagerInstance.Play(SFX.Impact);
//        }

//        StartCoroutine(WaitAndShowGameOver());
//    }

//    IEnumerator WaitAndShowGameOver()
//    {
//        yield return new WaitForSeconds(1.5f);

//        if (GameManager.Instance != null)
//            GameManager.Instance.GameOver();
//        else
//            Time.timeScale = 0f;
//    }
//}
