using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, jumpForce;

    private float timer = 0, jumpTime = 0.5f;
    private Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horiz = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horiz, 0, 0) * Time.deltaTime * speed;

        if (Input.GetButtonDown("Jump") && timer > jumpTime)
        {
            _rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
