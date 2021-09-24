using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        isWalking,
        isFalling,
        isGrounded,
        isStaggering
    }

    [SerializeField] private float speed, jumpVelocity; // made it private but still accessible through inspecter 
    [SerializeField] public PlayerState currentState;

    private float jumpCooldown = 0.4f, fallMultiplier = 2f, lowJumpMultiplier = 3.5f;
    private Rigidbody2D rb;
    public int jumpCharges, totalJumps = 5;
    public bool jumpReady;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = PlayerState.isGrounded;
        jumpCharges = totalJumps;
        jumpReady = true;
        StartCoroutine(DoubleJump(jumpCooldown));
    }

    void Update()
    {
        Movement();
    }

    private void ChangeState(PlayerState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void Movement()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(horiz, 0, 0) * Time.deltaTime * speed;

        //Changes gravity so you fall faster (makes it feel less floaty when jumping)
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //if jump is not held more gravity is applied causing a shorter jump (tap jump for less height, hold for more height)
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (currentState == PlayerState.isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpVelocity;
            ChangeState(PlayerState.isFalling);

        }
        else if (currentState != PlayerState.isGrounded && Input.GetButtonDown("Jump") && jumpReady == true && jumpCharges > 0)
        {
            jumpReady = false;
            rb.velocity = new Vector2(0.0f, 0.0f);
            rb.velocity = Vector2.up * jumpVelocity;
        }
    }
    private IEnumerator DoubleJump(float jumpCooldown)
    {
        while (true)
        {
            if (jumpReady == false)
            {
                yield return new WaitForSecondsRealtime(jumpCooldown);
                jumpCharges -= 1;
                jumpReady = true;
            }
            yield return null;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            jumpCharges = totalJumps;
            ChangeState(PlayerState.isGrounded);
        }
    }
}
