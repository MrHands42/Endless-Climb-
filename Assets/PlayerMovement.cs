using UnityEngine;

public class GridTeleportMovement2D : MonoBehaviour
{
    private const int GridSize = 3;
    
    private Vector2Int currentGridPos = new Vector2Int(0, 0);
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");   
        
        Vector2Int newGridPos = currentGridPos;
        if (horizontal != 0 && vertical == 0)
        {
            newGridPos.x += (int)horizontal;
        }
        else if (vertical != 0 && horizontal == 0)
        {
            newGridPos.y += (int)vertical;
        }
        
        if (newGridPos.x >= 0 && newGridPos.x < GridSize && newGridPos.y >= 0 && newGridPos.y < GridSize)
        {
            currentGridPos = newGridPos;
            transform.position = new Vector3(currentGridPos.x, currentGridPos.y, 0f);
        }
    }
}