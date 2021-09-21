using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed, jumpForce; // made it private but still accessible through inspecter 

    private float timer = 0, jumpTime = 0.4f;
    private Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Movement();
    }

    private void Movement()
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
