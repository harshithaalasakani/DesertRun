using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Drag the player (Timmy) into this slot in the Inspector
    public Vector3 offset = new Vector3(0, 10, -15);  // Adjusted offset for a better view

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
            transform.LookAt(player);  // Ensures the camera always looks at the player
        }
    }
}
