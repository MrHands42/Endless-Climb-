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
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartDash(0f, MoveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartDash(0f, -MoveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartDash(-MoveDistance, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartDash(MoveDistance, 0f);
            }
        }
    }

    private void StartDash(float deltaX, float deltaY)
    {
        AudioManager.AudioManagerInstance.Play(SFX.ChangeGrid);

        float newX = currentX + deltaX;
        float newY = currentY + deltaY;

        if (newX >= GridMin && newX <= GridMax && newY >= GridMin && newY <= GridMax)
        {
            StartCoroutine(DashCoroutine(newX, newY, deltaY));  // Tambahan: Pass deltaY ke coroutine
        }
    }

    private System.Collections.IEnumerator DashCoroutine(float targetX, float targetY, float deltaY)  // Tambahan: Parameter deltaY
    {
        isDashing = true;

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
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / DashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        currentX = targetX;
        currentY = targetY;

        isDashing = false;

        if (ScoreManager.instance != null && deltaY > 0)
        {
            ScoreManager.instance.AddVerticalScore(deltaY);
        }
    }
}