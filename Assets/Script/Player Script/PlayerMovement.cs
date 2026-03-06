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
    private bool isDead = false;  // Retained from previous fix
    private Vector3 posisiTerakhir;
    private Vector3 posisiAsliBody;

    public bool wantAudio = true;  // Pengaturan untuk mengaktifkan atau menonaktifkan audio, supaya ga double audionya. Hanya aktifkan di 1 tempat

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
            posisiAsliBody = playerBodyVisual.localPosition;
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
                timerDiam = 0f;
                ResetPosisiBody();
            }
            posisiTerakhir = transform.position;
        }

        // Part 2: Original PlayerMovement Update logic - Handle input, with isDead check to block movement
        if (isDead || isDashing)
        {
            return;  // Block input if dead or dashing
        }

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
    }

    // Merged from SlipMechanic: Fall method (kept as RopeSlip for compatibility)
    public void RopeSlip()
    {
        if (isDead) return;

        FindObjectOfType<Collector>().BreakShield();    // Memastikan player ga jatoh bawa shield + biar keren aja

        isDead = true;

        ResetPosisiBody();
        Debug.Log("Player Jatuh!");

        if (animator != null) animator.SetInteger("Direction", 7);

        // Disable self (PlayerMovement) to prevent movement
        enabled = false;

        if (GetComponent<Collider2D>() != null)
            GetComponent<Collider2D>().enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; 
            rb.gravityScale = 4f; 
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * 12f, ForceMode2D.Impulse);
        }

        if (AudioManager.AudioManagerInstance != null && wantAudio)
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
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Unflip()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void StartDash(float deltaX, float deltaY)
    {
        float newX = currentX + deltaX;
        float newY = currentY + deltaY;
        if (newX >= GridMin && newX <= GridMax && newY >= GridMin && newY <= GridMax)
        {
            StartCoroutine(DashCoroutine(newX, newY, deltaX, deltaY));
            if (AudioManager.AudioManagerInstance != null && wantAudio)
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

        if (animator != null)
        {
            animator.SetTrigger("Dash");
        }

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