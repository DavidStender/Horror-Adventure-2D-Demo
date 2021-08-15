using UnityEngine;

/**
 * An item that lets the player see inside the dark zone
 */
public class FlashLight : Item
{
    /**The sprite masked of the flashlight**/
    private SpriteMask flashlightOnMask;

    /**
     * Sets up the components used by the flashlight to function
     */
    private void Awake()
    {
        ItemComponentSetup();
    }

    /**
     * Calls the base use method. Also turns on and off the flashlightOnMask
     */
    public override void Use(Player user)
    {
        base.Use(user);
        flashlightOnMask.enabled = !flashlightOnMask.enabled;
    }

    /**
     * Contains the calls to set up the components need by the base class and the flashlight
     */
    public override void ItemComponentSetup()
    {
        base.ItemComponentSetup();
        flashlightOnMask = GetComponentInChildren<SpriteMask>();
    }

    /**
     * Deactivates the flashlightOnMask when the flashlight is unequipped 
     */
    public override void UnequipItem()
    {
        DeactivateFlashlight();
    }

    /**
     * Deactivates the flashlighs sprite mask
     */
    public void DeactivateFlashlight()
    {
        flashlightOnMask.enabled = false;
    }
}
