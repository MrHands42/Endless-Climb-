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

    void Start()
    {
        // Jika kamu lupa drag-drop di Inspector, script akan mencarinya sendiri
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        // Cek terakhir: Kalau masih gak ketemu juga, baru error
        if (animator == null)
        {
            Debug.LogError("ERROR PARAH: Tidak ada Animator di Player ataupun Anak-anaknya!");
        }
        else
        {
            Debug.Log("Animator found and assigned successfully.");
            animator.enabled = true; // Ensure it's enabled
        }
    }

    void Update()
    {
        if (!isDashing)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Key W pressed: Setting Direction to 1 (Up)");
                if (animator != null) animator.SetInteger("Direction", 1); // Up 
                StartDash(0f, MoveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("Key S pressed: Setting Direction to 2 (Down)");
                if (animator != null) animator.SetInteger("Direction", 2); // Down
                StartDash(0f, -MoveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Key A pressed: Setting Direction to 3 (Left)");
                if (animator != null) animator.SetInteger("Direction", 3); // Left
                StartDash(-MoveDistance, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Key D pressed: Setting Direction to 4 (Right)");
                if (animator != null) animator.SetInteger("Direction", 4); // Right
                StartDash(MoveDistance, 0f);
            }
        }
    }

    private void StartDash(float deltaX, float deltaY)
    {
        float newX = currentX + deltaX;
        float newY = currentY + deltaY;
        //Damn ngapain bacain komen
        if (newX >= GridMin && newX <= GridMax && newY >= GridMin && newY <= GridMax)
        {
            StartCoroutine(DashCoroutine(newX, newY, deltaY));
            AudioManager.AudioManagerInstance.Play(SFX.ChangeGrid);
        }
        else
        {
            Debug.Log("Dash invalid: Resetting to idle (Direction = 0)");
            if (animator != null) animator.SetInteger("Direction", 0);
        }
    }

    private System.Collections.IEnumerator DashCoroutine(float targetX, float targetY, float deltaY)
    {
        isDashing = true;
        Debug.Log("Dash started. Direction should be animating now.");

        if (animator != null)
        {
            animator.SetTrigger("Dash"); //Ngerti lahh
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

    }
}
