using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
<<<<<<< Updated upstream
    // Grid size: 3x3, positions from -1 to 1 in x and y
    private const int GridSize = 3;
    private const float MoveDistance = 1f; // Distance per move (matches grid spacing)

    // Current grid position (start at center)
    private int currentX = 0;
    private int currentY = 0;

    void Update()
    {
        // Get input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(0, 1); // Up
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(0, -1); // Down
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(-1, 0); // Left
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(1, 0); // Right
        }
    }

    private void Move(int deltaX, int deltaY)
    {
        // Calculate new position
        int newX = currentX + deltaX;
        int newY = currentY + deltaY;

        // Check bounds (grid is -1 to 1 in both axes)
        if (newX >= -1 && newX <= 1 && newY >= -1 && newY <= 1)
        {
            // Update position
            currentX = newX;
            currentY = newY;
            transform.position = new Vector3(currentX * MoveDistance, currentY * MoveDistance, 0);
        }
        // If out of bounds, do nothing (player stays put)
=======
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
        float newX = currentX + deltaX;
        float newY = currentY + deltaY;

        if (newX >= GridMin && newX <= GridMax && newY >= GridMin && newY <= GridMax)
        {
            StartCoroutine(DashCoroutine(newX, newY));
        }
    }

    private System.Collections.IEnumerator DashCoroutine(float targetX, float targetY)
    {
        isDashing = true;

        if (animator != null)
        {
            animator.SetTrigger("Dash");
        }

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
>>>>>>> Stashed changes
    }
}