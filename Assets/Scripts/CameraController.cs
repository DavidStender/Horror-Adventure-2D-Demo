using UnityEngine;

/**
 * A class used to contol moving the camera through the game world
 */
public class CameraController : MonoBehaviour
{
    /**The player of the game**/
    public Player player;

    /**
     * Moves the camera up, down, left, or right based on which directions the player is travels towards
     */
    private void LateUpdate()
    {
        if(player.transform.position.y >= transform.position.y + 8) // moves the camera up
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 16, transform.position.z);
        }
        else if (player.transform.position.y <= transform.position.y - 8) // moves the camera down
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 16, transform.position.z);
        }
        else if (player.transform.position.x >= transform.position.x + 10f) // moves the camera right
        {
            transform.position = new Vector3(transform.position.x + 20f, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.x <= transform.position.x - 10f) // moves the camera left
        {
            transform.position = new Vector3(transform.position.x - 20f, transform.position.y, transform.position.z);
        }
    }
}
