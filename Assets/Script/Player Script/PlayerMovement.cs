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

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDashing)
        {
            // Set direction animation on key press (starts immediately, persists during dash)
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                animator.SetInteger("Direction", 1); // Up - animation starts here and stays until dash ends
                StartDash(0f, MoveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator.SetInteger("Direction", 2); // Down
                StartDash(0f, -MoveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                animator.SetInteger("Direction", 3); // Left
                StartDash(-MoveDistance, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                animator.SetInteger("Direction", 4); // Right
                StartDash(MoveDistance, 0f);
            }
        }
    }

    private void StartDash(float deltaX, float deltaY)
    {
        float newX = currentX + deltaX;
        float newY = currentY + deltaY;

        if (newX >= GridMin && newX <= GridMax && newY >= GridMin && newY <= GridMax)
        {
            StartCoroutine(DashCoroutine(newX, newY, deltaY));
            AudioManager.AudioManagerInstance.Play(SFX.ChangeGrid);
        }
        else
        {
            // If dash is invalid (out of 3x3 grid bounds), reset to idle immediately
            animator.SetInteger("Direction", 0);
        }
    }

    private System.Collections.IEnumerator DashCoroutine(float targetX, float targetY, float deltaY)
    {
        isDashing = true;

        // Optional: Trigger a separate "Dash" animation if you have one (e.g., for effects during dash)
        if (animator != null)
        {
            animator.SetTrigger("Dash");
        }

        // Get start position
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

        // Reset to idle ONLY after the dash is fully over
        animator.SetInteger("Direction", 0);

        if (ScoreManager.instance != null && deltaY > 0)
        {
            ScoreManager.instance.AddVerticalScore(deltaY);
        }
    }
}