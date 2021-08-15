using UnityEngine;

/**
 * Handles the input that is used to move the player around the game world
 */
public class PlayerMovement : MonoBehaviour
{
    /**The speed at which the player moves**/
    public float movementSpeed = 1f;

    /**The current velocity the player is moving at**/
    private Vector2 currentVelocity;
    /**The rigidbody component of the player**/
    private Rigidbody2D rigidBody;
    /**The Player component of the player**/
    private Player player;
    /**The animator component of the player**/
    private Animator animator;

    /**
     * Sets up PlayerMovement while the component is being loaded
     */
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    /**
     * Checks the players playerState before looking for input
     */
    private void Update()
    {
        if (player.isAlive && player.playerState == PlayerState.EXPLORING)
            GetInput();
    }

    /**
     * Applys the players input to rigidbody components velocity to move the player around. If playerState
     * is not EXPLORING then the velocity is set to zero
     */
    private void FixedUpdate()
    {
        if (player.playerState != PlayerState.EXPLORING)
        {
            currentVelocity = Vector2.zero;
            animator.SetBool("NoInput", true);
        } 
        rigidBody.velocity = currentVelocity.normalized * movementSpeed;
    }

    /**
     * Reads input on the vertical and horizontal axis to move around the player and update the
     * animator component.
     */
    private void GetInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", horizontalInput);
        float verticalInput = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Vertical", verticalInput);

        Vector2 previousVelocity = currentVelocity;
        currentVelocity = new Vector2(horizontalInput, verticalInput);

        if (animator.GetFloat("Horizontal") == 0 && animator.GetFloat("Vertical") == 0 && !animator.GetBool("NoInput"))
            animator.SetBool("NoInput", true);
        else if (currentVelocity != previousVelocity)
        {
            animator.SetTrigger("NewInput");
            player.SetFacingDirection(currentVelocity);
        }
        else if (animator.GetBool("NoInput"))
            animator.SetBool("NoInput", false);
    }
}
