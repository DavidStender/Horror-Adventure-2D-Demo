using UnityEngine;

/**
 * A class that lets a player have a simple dialog with a game object
 */
public class DialogObject : MonoBehaviour, Interactable
{
    /**The text to be displayed**/
    public string dialog = "Placeholder";

    /**The game manager of the game**/
    private GameManager gameManager;

    /**
     * Sets up the components the dialog manager needs to function
     */
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    /**
     * Opens the dialog window and displays the dialog text when the player interacts
     * with the game object
     */
    public void Interact(Player player)
    {
        player.playerState = PlayerState.INTERACTING;
        gameManager.DisplayDialog(dialog);
    }
}
