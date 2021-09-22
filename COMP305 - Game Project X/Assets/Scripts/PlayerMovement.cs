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

    [SerializeField] private float speed, jumpForce; // made it private but still accessible through inspecter 
    [SerializeField] public PlayerState currentState;

    private float timer = 0, jumpTime = 0.4f;    
    private Rigidbody2D rb;      

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = PlayerState.isGrounded;
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
        float vert = rb.velocity.y;

        transform.position += new Vector3(horiz, 0, 0) * Time.deltaTime * speed;

        if (currentState == PlayerState.isGrounded && Input.GetButtonDown("Jump") && timer > jumpTime)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            ChangeState(PlayerState.isFalling);
            timer = 0;
        }

        if (currentState != PlayerState.isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);            
            Debug.Log("vert");
            timer = 0;
        }
        
        timer += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            ChangeState(PlayerState.isGrounded);
        }        
    }
}
