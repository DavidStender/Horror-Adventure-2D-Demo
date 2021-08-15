using UnityEngine;

/**
 * Added to an object that will be unlocked by a key in game
 */
public class KeyDoor : MonoBehaviour
{
    /**The key that unlocks the door**/
    public Key key; // Currently unused
    /**The message that is played when the door is unlocked**/
    public string unlockMessage;

    /**The dialog object that will display the unlockMessage**/
    private DialogObject dialogObject;

    /**
     * Sets up the necessary components of the KeyDoor while the object is being loaded
     */
    private void Awake()
    {
        dialogObject = GetComponent<DialogObject>();
    }

    /**
     * If collision is a key then the unlock message is displayed, the player is set to interacting,
     * and the KeyDoor is disabled
     * 
     * Collider2D (collision): The object that collided with the KeyDoor trigger
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Key>())
        {
            dialogObject.dialog = unlockMessage;
            dialogObject.Interact(collision.GetComponentInParent<Player>());
            gameObject.SetActive(false);
        }
    }
}
