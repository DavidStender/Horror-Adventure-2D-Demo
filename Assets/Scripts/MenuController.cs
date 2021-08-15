using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * Handles control of the player inventory menu such as displaying the items in the players inventory and
 * setting the player's itemUseSlots
 */
public class MenuController : MonoBehaviour
{
    /**The UI game object that contains the menu UI items**/
    public GameObject menuGameObject;
    /**The menu inventory slots that will dispaly and hold the players items**/
    public GameObject[] menuItemSlot;

    /**The player of the game**/
    private Player player;
    /**The games canvas event system component**/
    private EventSystem canvasEventSystem;

    /**
     * Sets up the necessary components of the MenuController while the object is being loaded
     */
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        canvasEventSystem = GameObject.FindGameObjectWithTag("Event System").GetComponent<EventSystem>();
        menuGameObject.SetActive(true);
        menuGameObject.SetActive(false);
    }

    /**
     * Constantly checks if the player has pressed the menu button
     */
    private void Update()
    {
        HandleMenuingInput();
    }

    /**
     * When the menu button is pressed the player's playerState is updated, the menuGameObject is activated, and
     * the inventory screen is built from the items in the players inventory
     */
    public void OpenMenu()
    {
        player.SetPlayerState(PlayerState.MENUING);
        menuGameObject.SetActive(true);
        BuildInventoryMenuScreen();
    }

    /**
     * Deactivates the UI menuGameObject, updates the canvasEvenetSystems first selected gameObject, deselects the currently
     * select menu item and updates the players playerState
     */
    public void CloseMenu()
    {
        menuGameObject.SetActive(false);
        canvasEventSystem.firstSelectedGameObject = canvasEventSystem.currentSelectedGameObject;
        canvasEventSystem.SetSelectedGameObject(null);
        player.SetPlayerState(PlayerState.EXPLORING);
    }

    /**
     * Scans the players inventory and builds the menu screen with the items they currently possess
     * 
     * CURRENT LIMITAION: Item Inventory Slot prefabs must be added manually to the item Menu gameObject as children
     * for each item the play can pick up
     */
    public void BuildInventoryMenuScreen()
    {
        Item[] playerInventory = player.GetPlayerInventory();
        for(int i=0; i<playerInventory.Length; i++)
        {
            if(playerInventory[i] != null)
            {
                menuItemSlot[i].SetActive(true);
                menuItemSlot[i].GetComponentInChildren<Image>().sprite = playerInventory[i].sprite;
                menuItemSlot[i].GetComponentInChildren<Text>().text = playerInventory[i].itemName;
                menuItemSlot[i].GetComponent<MenuItemSlot>().itemSlot = playerInventory[i];
            }  
        }

        if (playerInventory[0] != null && canvasEventSystem.firstSelectedGameObject == null)
            menuItemSlot[0].GetComponent<Selectable>().Select();
        else if (canvasEventSystem.firstSelectedGameObject != null)
            canvasEventSystem.SetSelectedGameObject(canvasEventSystem.firstSelectedGameObject);
    }

    /**
     * Reads the current playerState of the player and handles input accordingly
     */
    private void HandleMenuingInput()
    {
        if(player.playerState == PlayerState.EXPLORING)
        {
            if(Input.GetButtonDown("Menu"))
            {
                OpenMenu();
            }
        }
        else if (player.playerState == PlayerState.MENUING)
        {
            if (Input.GetButtonDown("Menu"))
            {
                CloseMenu();
            }
            else if (Input.GetButtonDown("ItemOne"))
            {
                Debug.Log("Putting item on slot 1");
                EquipItemToSlot(0);
            }
            else if (Input.GetButtonDown("ItemTwo"))
            {
                Debug.Log("Putting item on slot 2");
                EquipItemToSlot(1);
            }
            else if (Input.GetButtonDown("ItemThree"))
            {
                Debug.Log("Putting item on slot 3");
                EquipItemToSlot(2);
            }
        }
    }

    /**
     * Equips an item to the players itemUseSlot at slotNumber
     * 
     * int (slotNumber): The itemUseSlot to equip the item to
     */
    private void EquipItemToSlot(int slotNumber)
    {
        if(canvasEventSystem.currentSelectedGameObject != null)
            player.SetItemUseSlot(canvasEventSystem.currentSelectedGameObject.GetComponent<MenuItemSlot>().itemSlot, slotNumber);
    }
}
