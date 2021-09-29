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
        isStaggering
    }

    [SerializeField] private float speed, jumpVelocity; // made it private but still accessible through inspecter 
    [SerializeField] public PlayerState currentState;

    private float jumpCooldown = 0.2f, fallMultiplier = 2f, lowJumpMultiplier = 3.5f;
    private Rigidbody2D rb;
    public int jumpCharges, totalJumps = 3, maxSpeed = 10;
    public bool jumpReady;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = PlayerState.isGrounded;
        jumpCharges = totalJumps;
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
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        animator.SetInteger("Speed", horizontal);
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
            rb.velocity = Vector2.up * jumpVelocity;
            ChangeState(PlayerState.isJumping);

        }
        //double jump
        else if ((currentState == PlayerState.isFalling || currentState == PlayerState.isJumping) && Input.GetButtonDown("Jump") && jumpReady == true && jumpCharges > 0)
        {
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
    }
    private IEnumerator DoubleJumpCooldown(float jumpCooldown)
    {
        while (true)
        {
            if (jumpReady == false)
            {
                jumpCharges -= 1;
                PlayerUI.jumpsLeft = jumpCharges;
                yield return new WaitForSecondsRealtime(jumpCooldown);
                jumpReady = true;

            }
            yield return null;
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            PlayerUI.jumpsLeft = totalJumps;
            jumpCharges = totalJumps;
            ChangeState(PlayerState.isGrounded);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            string name = other.gameObject.name;
            if (name.Equals("WallLeft"))
            {
                animator.SetInteger("WallSlide", -1);
            }
            else
            {
                animator.SetInteger("WallSlide", 1);
            }
        }
        if (other.gameObject.CompareTag("Spike"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            ChangeState(PlayerState.isDblJumping);
            rb.velocity = new Vector2(0.0f, 0.0f);
            rb.velocity = Vector2.up * jumpVelocity;
            jumpCharges = totalJumps;
            PlayerUI.jumpsLeft = jumpCharges;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        //once player leaves the wall collider he will no longer be sliding
        if (other.gameObject.CompareTag("Wall"))
        {
            animator.SetInteger("WallSlide", 0);
        }
    }
}
