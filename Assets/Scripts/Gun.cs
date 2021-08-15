using UnityEngine;

/**
 * A weapon that gives the player a ranged attack
 */
public class Gun : Weapon
{
    /**
     * Shoots a bullet in the direction the player is facing
     * 
     * user (Player): The user of the item
     */
    public override void Use(Player user)
    {
        //Bug? lets the player shoot diagonal
        base.Use(user);
        RaycastHit2D hit;
        if(hit = Physics2D.Raycast(transform.position, user.GetFacingDirection(), 8f, 1024))
        {
            hit.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
