using UnityEngine;
using UnityEngine.UI;

/**
 * Handles updating the players UI elements
 */
public class PlayerUIController : MonoBehaviour
{
    /**The UI elements showing what the player has equipped**/
    public Image[] itemUseSlotUI;

    /**
     * Updates the players itemUseSlot UI to the item just equipped
     * 
     * int (slotNumber): The slot to update
     * Sprite (itemSprite): The image to be displayed on the UI
     */
    public void SetItemUseSlotImage(int slotNumber, Sprite itemSprite)
    {
        if(slotNumber >= 0 && slotNumber < itemUseSlotUI.Length)
        {
            if(itemSprite == null) // Uses in the beginning of the game while the player has less then 3 items to equip
            {
                itemUseSlotUI[slotNumber].enabled = false;
            }
            else
            {
                itemUseSlotUI[slotNumber].enabled = true;
                itemUseSlotUI[slotNumber].sprite = itemSprite;
            }
        }
    }
}
