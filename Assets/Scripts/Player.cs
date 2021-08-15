using UnityEngine;

/**
 * The different states the player can be in
 */
public enum PlayerState
{
    EXPLORING,
    INTERACTING,
    MENUING,
    USINGITEM
}

/**
 * Turns a gameObject into a player. Lets them interact with world. Has an iventory, and item slots to use.
 */
public class Player : MonoBehaviour
{
    /**If the player is alive or not**/
    public bool isAlive = true;
    /**Items that the player currently has equipped**/
    public Item[] itemUseSlots;
    /**All the items the player currently has**/
    public Item[] inventory;
    /**The current state of the player**/
    public PlayerState playerState;

    /**The direction the player is currently facing**/
    private Vector2 facingDirection;
    /**The component that controls the players UI elements**/
    private PlayerUIController playerUIController;
    /**The component that helps control the game**/
    public GameManager gameManager;

    /**
     * Sets up the player while the component is being loaded
     */
    void Awake()
    {
        // Overwrites the inspector at run time
        // remove comment later?
        // itemUseSlots = new Item[3];
        playerState = PlayerState.EXPLORING;
        facingDirection = new Vector2(-1, 0);
        playerUIController = GetComponent<PlayerUIController>();
    }

    /**
     * Changes what the player is able to do based on the current state of playerState
     */
    void Update()
    {
        if(isAlive && playerState == PlayerState.EXPLORING)
        {
            HandleExploringInput();
        } 
        else if(playerState == PlayerState.INTERACTING)
        {
            HandleInteractingInput();
        }
    }

    /**
     * Handles non movement input from the player while playerState is EXPLORING.
     * Handles using items and initiating interaction input
     */
    private void HandleExploringInput()
    {
        if (Input.GetButtonDown("ItemOne"))
        {
            if (itemUseSlots[0] != null)
                itemUseSlots[0].Use(this);
        }
        else if (Input.GetButtonDown("ItemTwo"))
        {
            if (itemUseSlots[1] != null)
                itemUseSlots[1].Use(this);
        }
        else if (Input.GetButtonDown("ItemThree"))
        {
            if (itemUseSlots[2] != null)
                itemUseSlots[2].Use(this);
        }
        else if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    /**
     * Handles non movement input from the player while playerState is INTERACTING
     */
    private void HandleInteractingInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (!gameManager.ContinueDialog())
                playerState = PlayerState.EXPLORING;
        }
    }

    /**
     * Sets playerState to equal EXPLORING
     */
    public void SetPlayerStateExploring()
    {
        playerState = PlayerState.EXPLORING;
    }

    /**
     * Sets playerState to be equal to newPlayerState
     * 
     * PlayerState (newPlayerState): The new state for the player
     */
    public void SetPlayerState(PlayerState newPlayerState)
    {
        playerState = newPlayerState;
    }

    /**
     * Shoots a 2D raycast in the facingDirection of the player. If it hits something it looks for an Interactable component on it and sets
     * the playerState to INTERACTING
     */
    private void Interact()
    {
        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(transform.position, facingDirection, 1f, 256))
        {
            hit.transform.GetComponent<Interactable>().Interact(this);
            playerState = PlayerState.INTERACTING;
        }
    }

    /**
     * Returns the direction the player is facing in Vector2
     */
    public Vector2 GetFacingDirection()
    {
        return facingDirection;
    }

    /**
     * Sets facingDirection to be equal to newDirection
     * 
     * Vector2 (newDirection): The new direction the player is facing
     */
    public void SetFacingDirection(Vector2 newDirection)
    {
        facingDirection = newDirection;
    }

    /**
     * Adds item to the players inventory in the first null position. Will also
     * add the item to the players itemUseSlots if they are not full.
     * 
     * Item (item): The item being added to the players inventory
     * 
     * Returns if the item was added to the players inventory
     */
    public bool AddItemToInventory(Item item)
    {
        for(int i=0; i<inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = item;
                int itemSlot = CheckItemUseSlots();
                if(itemSlot >= 0)
                {
                    SetItemUseSlot(item, itemSlot);
                }
                return true;
            }
        }
        return false;
    }

    /**
     * Checks if any of the itemUseSlots are empty. If they are empty the position of the empty slot in the array
     * is returned. Otherwise, -1 is return if the currently have items in them.
     * 
     * Returns if any of the itemUseSlots are empty
     */
    private int CheckItemUseSlots()
    {
        for(int i=0; i<itemUseSlots.Length; i++)
        {
            if (itemUseSlots[i] == null)
                return i;
        }
        return -1;
    }

    /**
     * Sets itemUseSlot at slotNumber to be item. If the item is already in the itemUseSlots and the player sets it
     * to a different slot then it currently is then the items in those locations will be swapped.
     * 
     * Item (item): The item being set in the itemUseSlot
     * int (slotNumber): The itemUseSlot position the item will be set to.
     */
    public void SetItemUseSlot(Item item, int slotNumber)
    {
        int itemSlotCheck = CheckItemAlreadyInItemSlot(item);
        if(itemSlotCheck < 0)
        {
            if (itemUseSlots[slotNumber] != null)
                itemUseSlots[slotNumber].UnequipItem();
            itemUseSlots[slotNumber] = item;
            playerUIController.SetItemUseSlotImage(slotNumber, item.sprite);
        }
        else
        {
            SwapItemSlots(slotNumber, itemSlotCheck);
        }
    }

    /**
     * SetItemUseSlot() helper method. Checks if item is already in the players itemUseSlots. If the item is found
     * its position in the array is return. Otherwise, -1 is returned.
     * 
     * Returns -1 if item is not found in itemUseSlots else if the item is found its position in the array is returned
     */
    private int CheckItemAlreadyInItemSlot(Item item)
    {
        for(int i=0; i<itemUseSlots.Length; i++)
        {
            if(itemUseSlots[i] == item)
            {
                return i;
            }
        }
        return -1;
    }

    /**
     * SetItemUseSlot() helper method. Takes 2 items currently in the players itemUseSlots and
     * swaps their positions in the itemUseSlots.
     * 
     * int (slotNumber1): The position of one of the items in itemUseSlots
     * int (slotNumber2): The position of the other item in itemUseSlots
     */
    private void SwapItemSlots(int slotNumber1, int slotNumber2)
    {
        Item tempItem = itemUseSlots[slotNumber1];
        itemUseSlots[slotNumber1] = itemUseSlots[slotNumber2];
        playerUIController.SetItemUseSlotImage(slotNumber1, itemUseSlots[slotNumber1].sprite);
        itemUseSlots[slotNumber2] = tempItem;
        playerUIController.SetItemUseSlotImage(slotNumber2, itemUseSlots[slotNumber2] != null ? itemUseSlots[slotNumber2].sprite : null);
    }

    /**
     * Returns the items currently in the players inventory
     */
    public Item[] GetPlayerInventory()
    {
        return inventory;
    }
}
