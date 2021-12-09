using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMover : MonoBehaviour
{
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0.0f);
        }

        if (transform.position.x > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0.0f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
