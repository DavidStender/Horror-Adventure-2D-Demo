using System.Collections;
using UnityEngine;

/**
 * A Class to be used as a base to create enemies in the game
 */
public class Enemy : MonoBehaviour
{
    /**The amount of health the enemy has**/
    public int hp;
    /**The sound played when the enemy gets hit**/
    public AudioClip hitSound;

    /**The Sprite Renderer of the enemy**/
    private SpriteRenderer spriteRenderer;
    /**The audio source component of the enemy**/
    private AudioSource audioSource;

    /**
     * Sets up the necessary components for for all enemy subclasses
     */
    private void Awake()
    {
        EnemyComponentSetup();
    }

    /**
     * Handles the enemy taking damage
     * 
     * damageAmount (int): The amount of damage to deal to the enemy
     */
    public void TakeDamage(int damageAmount)
    {
        //Incomplete does not actually apply any damage to the enemy
        Debug.Log($"Enemy {name} takes {damageAmount} damage.");
        audioSource.PlayOneShot(hitSound);
        StartCoroutine("DamageFlash");
    }

    /**
     * Flashes the sprite renderer of the enemy to show it taking damage
     */
    IEnumerator DamageFlash()
    {
        for(int i=0; i<1; i++)
        {
            spriteRenderer.color = Color.black;
            yield return new WaitForSecondsRealtime(0.05f);
            spriteRenderer.color = Color.gray;
            yield return new WaitForSecondsRealtime(0.05f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    /**
     * Sets up the necessary components of the base class and can be overridden by subclasses to add
     * components necessary to them
     */
    public virtual void EnemyComponentSetup()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
}
