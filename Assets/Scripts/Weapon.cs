using UnityEngine;

/**
 * An item used to attack and damage enemies
 */
public class Weapon : Item
{
    /**The amount of damage dealt to an enemy when used**/
    public int damage;

    /**
     * If the gameObject that was collided with is an enemy calls the TakeDamage() method.
     * Otherwise just calls the base CollisionHandler
     * 
     * GameObject (gameObject): The gameObject being collided with
     */
    public override void CollisionHandler(GameObject gameObject)
    {
        GameObject hitGameObject = gameObject.transform.root.gameObject;
        if (gameObject.gameObject.transform.root.GetComponent<Enemy>())
        {
            gameObject.gameObject.transform.root.GetComponent<Enemy>().TakeDamage(damage);
        }
        else if(gameObject.transform.root.GetComponent<ICuttable>() is ICuttable)
        {
            gameObject.transform.root.GetComponent<ICuttable>().Cut(this.transform.parent.gameObject);
        }
        else
            base.CollisionHandler(gameObject);
    }
}
