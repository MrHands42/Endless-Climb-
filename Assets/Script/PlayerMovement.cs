using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
    }
}