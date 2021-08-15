using UnityEngine;

/**
 * A component that creates a bush gameobject that is cuttable by the players knife
 */
public class Bush : MonoBehaviour, ICuttable
{
    /**The Animator component of the Bush**/
    private Animator animator;

    /**
     * Sets up the necessary components while the script is being loaded
     */
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /**
     * Is called when the knife weapon cuts the Bush
     * 
     * cutter (GameObject): The player who cut the Bush
     */
    public void Cut(GameObject cutter)
    {
        animator.SetTrigger("Cut");
    }

    /**
     * Sets the Bush's active state to be false
     */
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
