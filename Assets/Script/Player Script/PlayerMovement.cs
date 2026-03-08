using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float MoveDistance = 2.5f;
    private const float GridMin = -2.5f; 
    private const float GridMax = 2.5f;  
    private float currentX = 0f;
    private float currentY = 0f;

    private const float DashDuration = 0.2f;
    private bool isDashing = false;

    [Header("Visuals")]
    public Animator animator;
    public SpriteRenderer spriteVisual;

    // Merged from SlipMechanic: Idle detection and fall settings
    [Header("Slip Mechanic Settings")]
    public float batasWaktuDiam = 7f;
    public float toleransiGerak = 0.001f;

    [Header("Vibration Settings")]
    public Transform playerBodyVisual;
    public float kekuatanGetar = 0.5f;

    // Merged from SlipMechanic: Private variables for idle/fall logic
    private float mulaiGetarDetik;
    private float timerDiam = 0f;
    public bool isDead = false;  // Retained from previous fix
    private Vector3 posisiTerakhir;
    private Vector3 posisiAsliBody;

    private Rigidbody2D rb;
    void Start()
    {
        // Original PlayerMovement Start logic
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("ERROR PARAH: Tidak ada Animator di Player ataupun Anak-anaknya!");
        }
        else
        {
            Debug.Log("Animator found and assigned successfully.");
            animator.enabled = true;
        }

        // Merged from SlipMechanic Start: Initialize idle detection
        mulaiGetarDetik = batasWaktuDiam * (2f / 3f);
        posisiTerakhir = transform.position;
        if (playerBodyVisual != null)
        {
            posisiAsliBody = playerBodyVisual.localPosition;
        }
    }

    // SINGLE Update method: Merges idle detection/vibration (from SlipMechanic) with input handling (from original PlayerMovement)
    void Update()
    {
        // Part 1: Merged from SlipMechanic Update - Handle idle detection and vibration
        if (!isDead)
        {
            float jarakGerak = Vector3.Distance(transform.position, posisiTerakhir);
            if (jarakGerak <= toleransiGerak)
            {
                timerDiam += Time.deltaTime;
                if (timerDiam > mulaiGetarDetik) GetarkanBody();
                if (timerDiam >= batasWaktuDiam) RopeSlip();
            }
            else
            {
                if (timerDiam > 0f)
                {
                    timerDiam = 0f;
                    ResetPosisiBody();
                }
            }
            posisiTerakhir = transform.position;
        }

        // Part 2: Original PlayerMovement Update logic - Handle input, with isDead check to block movement
        if (isDead || isDashing || ButtonManager.isPaused)
        {
            return;  // Block input if dead or dashing
        }

        if (!isDead) {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Key W pressed: Setting Direction to 1 (Up)");
                if (animator != null) animator.SetInteger("Direction", 1);
                StartDash(0f, MoveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("Key S pressed: Setting Direction to 2 (Down)");
                if (animator != null) animator.SetInteger("Direction", 2);
                StartDash(0f, -MoveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Key A pressed: Setting Direction to 3 (Left)");
                if (animator != null) animator.SetInteger("Direction", 3);
                StartDash(-MoveDistance, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Key D pressed: Setting Direction to 4 (Right)");
                if (animator != null) animator.SetInteger("Direction", 4);
                StartDash(MoveDistance, 0f);
            }
        }
    }

    // Merged from SlipMechanic: Vibration methods
    void GetarkanBody()
    {
        if (playerBodyVisual != null)
        {
            float intensity = kekuatanGetar * (timerDiam / batasWaktuDiam);
            Vector3 randomShake = (Vector3)Random.insideUnitCircle * intensity;
            playerBodyVisual.localPosition = posisiAsliBody + randomShake;
        }
    }

    void ResetPosisiBody()
    {
        if (playerBodyVisual != null) playerBodyVisual.localPosition = posisiAsliBody;
        Debug.Log("Player moved: Resetting idle timer and body position.");
    }

    // Merged from SlipMechanic: Fall method (kept as RopeSlip for compatibility)
    virtual public void RopeSlip()
    {
        if (isDead) return;
        isDead = true;
        StopAllCoroutines();
        FindObjectOfType<Collector>().BreakShield();    // Memastikan player ga jatoh bawa shield + biar keren aja

        //ResetPosisiBody();

        Debug.Log("Player Jatuh!");

        if (animator != null) animator.SetInteger("Direction", 7);

        // Disable self (PlayerMovement) to prevent movement
        enabled = false;

        if (GetComponent<Collider2D>() != null)
            GetComponent<Collider2D>().enabled = false;


        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 4f;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 12f, ForceMode2D.Impulse);

        if (AudioManager.AudioManagerInstance != null)
        {
            AudioManager.AudioManagerInstance.Play(SFX.Impact);
            StartCoroutine(WaitAndShowGameOver());
        }        
    }

    // Merged from SlipMechanic: Game over coroutine
    IEnumerator WaitAndShowGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        if (GameManager.Instance != null)
            GameManager.Instance.GameOver();
        else
            Time.timeScale = 0f;
    }

    // Original PlayerMovement methods (unchanged)

    private void Flip()
    {
        if (spriteVisual != null)   // Kalau spriteVisual udah di-assign
        {
            spriteVisual.flipX = true;
        }
        else // Ga ada spriteVisual
        {
            SpriteRenderer cadanganVisual = GetComponentInChildren<SpriteRenderer>();
            if (cadanganVisual != null) // Kalau animator gaada
            {
                cadanganVisual.flipX = true;
            }
        }
    }

    private void Unflip()
    {
        if (spriteVisual != null)
        {
            spriteVisual.flipX = false;
        }
        else
        {
            SpriteRenderer cadanganVisual = GetComponentInChildren<SpriteRenderer>();
            if (cadanganVisual != null)
            {
                cadanganVisual.flipX = false;
            }
        }
    }

    private void StartDash(float deltaX, float deltaY)
    {
        float newX = currentX + deltaX;
        float newY = currentY + deltaY;
        if (newX >= GridMin && newX <= GridMax && newY >= GridMin && newY <= GridMax)
        {
            StartCoroutine(DashCoroutine(newX, newY, deltaX, deltaY));
            if (AudioManager.AudioManagerInstance != null)
            {
                AudioManager.AudioManagerInstance.Play(SFX.ChangeGrid);
            }    
        }
        else
        {
            Debug.Log("Dash invalid: Resetting to idle (Direction = 0)");
            if (animator != null) animator.SetInteger("Direction", 0);
        }
    }

    private System.Collections.IEnumerator DashCoroutine(float targetX, float targetY, float deltaX, float deltaY)
    {
        isDashing = true;
        Debug.Log("Dash started. Direction should be animating now.");

        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(targetX, targetY, 0f);

        float elapsedTime = 0f;
        while (elapsedTime < DashDuration)
        {
            while (Time.timeScale == 0) yield return null; 
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / DashDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.position = targetPos;

        currentX = targetX;
        currentY = targetY;

        isDashing = false;

        Debug.Log("Dash ended: Resetting to idle (Direction = 0)");
        if (animator != null) animator.SetInteger("Direction", 0);

        if (deltaX < 0)
        {
            Unflip();
        }
        else if (deltaX > 0)
        {
            Flip();
        }

        // if (ScoreManager.instance != null && deltaY > 0)
        // {
        //     ScoreManager.instance.AddVerticalScore(deltaY);
        // }
    }
}