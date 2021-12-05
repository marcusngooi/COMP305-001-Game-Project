using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        isRunning,
        isJumping,
        isDblJumping,
        isFalling,
        isGrounded,
        isStaggering,
        isDead
    }

    [SerializeField] private float speed, jumpVelocity; // made it private but still accessible through inspecter 
    [SerializeField] public PlayerState currentState;
    [SerializeField] GameObject gameController,jump;

    private float jumpCooldown = 0.2f, fallMultiplier = 2f, lowJumpMultiplier = 3.5f;
    private Rigidbody2D rb;
    public int jumpCharges, totalJumps = 3, maxSpeed = 8;
    public bool jumpReady, godMode;
    public Animator animator;
    public CapsuleCollider2D playerBody;
    public BoxCollider2D playerFeet;
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    public AudioClip landAudio;
    public AudioClip jumpAudio;
    public AudioClip eDeathAudio;
    public AudioClip playerDeathAudio;
    public ParticlesController playerParticles;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentState = PlayerState.isGrounded;
        jumpCharges = totalJumps;
        jump.GetComponent<Jumps>().jump = totalJumps;
        jumpReady = true;
        StartCoroutine(DoubleJumpCooldown(jumpCooldown));
    }
    void Update()
    {
        Movement();
        Animation();
    }

    private void ChangeState(PlayerState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void Animation()
    {
        int horizontal = Mathf.Abs((int)Input.GetAxisRaw("Horizontal"));
        animator.SetInteger("Speed", horizontal);
        /*if (currentState == PlayerState.isDead)
        {
            animator.SetTrigger("Death");
            StartCoroutine(PlayerDeath());
        }*/
        if (currentState == PlayerState.isFalling)
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }

        if (currentState == PlayerState.isJumping)
        {
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }

        if (currentState == PlayerState.isGrounded)
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
        if (currentState == PlayerState.isDblJumping)
        {
            animator.SetBool("DblJumping", true);
        }
        else
        {
            animator.SetBool("DblJumping", false);
        }
        
    }
    private void Movement()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horiz * speed * Time.fixedDeltaTime, rb.velocity.y);

        //Changes gravity so you fall faster (makes it feel less floaty when jumping)
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            ChangeState(PlayerState.isFalling);
        }
        //if jump is not held more gravity is applied causing a shorter jump (tap jump for less height, hold for more height)
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        //jump
        if (currentState == PlayerState.isGrounded && Input.GetButtonDown("Jump"))
        {
            audioSource.PlayOneShot(jumpAudio);
            playerParticles.CreateDust();
            rb.velocity = Vector2.up * jumpVelocity;
            ChangeState(PlayerState.isJumping);
        }
        //double jump
        else if ((currentState == PlayerState.isFalling || currentState == PlayerState.isJumping) && Input.GetButtonDown("Jump") && jumpReady == true && jumpCharges > 0)
        {
            audioSource.PlayOneShot(jumpAudio);
            ChangeState(PlayerState.isDblJumping);
            jumpReady = false;
            rb.velocity = new Vector2(0.0f, 0.0f);
            rb.velocity = Vector2.up * jumpVelocity;
        }
        //speed cap
        if (rb.velocity.y < -maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxSpeed);
        }
        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            godMode = !godMode;
        }*/
        if (horiz > 0 && currentState != PlayerState.isDead)
        {
            spriteRenderer.flipX = false;
        } else if(horiz < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    private IEnumerator DoubleJumpCooldown(float jumpCooldown)
    {
        while (true)
        {
            if (jumpReady == false)
            {
                jumpCharges -= 1;
                jump.GetComponent<Jumps>().jump -= 1;
                yield return new WaitForSecondsRealtime(jumpCooldown);
                jumpReady = true;
            }
            yield return null;
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") && other.otherCollider == playerFeet)
        {
            audioSource.PlayOneShot(landAudio);
            jump.GetComponent<Jumps>().jump = totalJumps;
            jumpCharges = totalJumps;
            ChangeState(PlayerState.isGrounded);
        }
        if ((other.gameObject.CompareTag("Spike") || other.gameObject.CompareTag("Laser")) && godMode == false)
        {
            StartCoroutine(PlayerDeath());
            gameController.GetComponent<GameController>().calculateScore();
        }
        if(other.gameObject.CompareTag("Enemy") && other.otherCollider == playerBody && godMode == false)
        {
            StartCoroutine(PlayerDeath());
            gameController.GetComponent<GameController>().calculateScore();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMovement>().Death();
            audioSource.PlayOneShot(eDeathAudio);
            ChangeState(PlayerState.isJumping);
            rb.velocity = new Vector2(0.0f, 0.0f);
            rb.velocity = Vector2.up * jumpVelocity;
            jumpCharges = totalJumps;
            jump.GetComponent<Jumps>().jump = totalJumps;
            gameController.GetComponent<GameController>().enemyKC += 1;
        }
        if (other.gameObject.CompareTag("FinishLine"))
        {
            gameController.GetComponent<GameController>().calculateScore();
        }
    }

   
    private IEnumerator PlayerDeath()
    {
        ChangeState(PlayerState.isDead);
        var collidersObj = gameObject.GetComponentsInChildren<Collider2D>();
        for (var index = 0; index < collidersObj.Length; index++)
        {
            var colliderItem = collidersObj[index];
            colliderItem.enabled = false;
        }
        audioSource.PlayOneShot(playerDeathAudio);
        animator.SetTrigger("Death");
        playerParticles.DeathEffect();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
