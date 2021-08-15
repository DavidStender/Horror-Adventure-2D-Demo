using UnityEngine;

/**
 * A class that is used to activate and deactivate the dark zone sprite. Added to a 2D trigger collider.
 */
public class DarkZoneTrigger : MonoBehaviour
{
    /**The dark zone's sprite renderer**/
    public SpriteRenderer darkZoneSprite;

    /**
     * Activates the dark zone when the player enters the trigger
     * 
     * collision (Collider2D): The game object that entered the trigger collider
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.gameObject.tag == "Player")
        {
            darkZoneSprite.enabled = true;
        }
    }

    /**
     * Deactivates the dark zone when the player leaves the trigger
     * 
     * collision (Collider2D): The game object that exited the trigger collider
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.root.gameObject.tag == "Player")
        {
            darkZoneSprite.enabled = false;
        }
    }
}
