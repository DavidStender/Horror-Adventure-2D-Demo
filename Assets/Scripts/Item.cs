using UnityEngine;

/**
 * Equipment that the player can pick up and use around the game world
 */
public class Item : MonoBehaviour
{
    /**The name of the item**/
    public string itemName;
    /**The description of the item**/
    public string description;
    /**The image displayed in the item inventory**/
    public Sprite sprite;
    /**What animation the player uses**/
    public string animatorTriggerName;
    /**The sound played when the player picks up the item**/
    public AudioClip pickupSound;
    /**The sound played when the playe uses the item**/
    public AudioClip useSound;

    /**The animator component of the item**/
    private Animator animator;
    /**The rigidbody 2d component of the item**/
    private Rigidbody2D rigidBody;
    /**The collider used when the item is in the overworld**/
    private BoxCollider2D boxCollider2D;
    /**The sprite renderer component of the item**/
    private SpriteRenderer spriteRenderer;
    /**The audio source component of the item**/
    private AudioSource audioSource;

    /**
     * Sets up all the necessary components when the script is being loaded
     */
    private void Awake()
    {
        ItemComponentSetup();
    }

    /**
     * Allows the player to use an item from their itemUseSlots. Updates the player's playersState and animator.
     * Sets the animator trigger, updates the facing direction of the item, and plays the useSound.
     * 
     * Player (user): The user of the item
     */
    public virtual void Use(Player user)
    {
        user.playerState = PlayerState.USINGITEM;
        user.GetComponent<Animator>().SetTrigger(animatorTriggerName);
        animator.SetTrigger(animatorTriggerName);

        Vector2 playerFacingDirection = user.GetFacingDirection();
        animator.SetFloat("Horizontal", playerFacingDirection.x);
        animator.SetFloat("Vertical", playerFacingDirection.y);
        PlayUseSound();
    }

    /**
     * Addes the item to the players inventory. When the item is added it is set as a child
     * of the player, its sprite renderer is disabled, and the pickupSound is played.
     * 
     * Player (player): The player whos inventory the item is added to.
     */
    public void Pickup(Player player)
    {
        if(player.AddItemToInventory(this))
        {
            GetComponentInChildren<BoxCollider2D>().enabled = false;
            transform.parent = player.gameObject.transform;
            transform.localPosition = Vector3.zero;
            spriteRenderer.enabled = false;
            PlayPickupSound();
        }
    }

    /**
     * Calls the items collision handler method when an object collides with it
     * 
     * Collider2D (other): The object that collided with the item
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        CollisionHandler(other.gameObject);
    }

    /**
     * If collisionObject was the Player then the pickup method is called
     * 
     * GameObject (collisionObject): The object that collided with the item
     */
    public virtual void CollisionHandler(GameObject collisionObject)
    {
        if (collisionObject.transform.root.tag == "Player")
        {
            Pickup(collisionObject.transform.root.GetComponent<Player>());
        }
    }

    /**
     * Adjusts the volume of the audio source component and playes the pickupSound
     */
    private void PlayPickupSound()
    {
        audioSource.volume = 0.25f;
        audioSource.PlayOneShot(pickupSound);
    }

    /**
     * Adjusts the volume of the audio source component and playes the useSound
     */
    private void PlayUseSound()
    {
        audioSource.volume = 0.45f;
        audioSource.PlayOneShot(useSound);
    }

    /**
     * Sets up all the necessary components that the item requires
     */
    public virtual void ItemComponentSetup()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponentInChildren<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    /**
     * Called when an item is removed from the players itemUseSlots
     */
    public virtual void UnequipItem()
    {
        // Unused in the base class
    }
}
