using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> waypoints;
    
    public float moveSpeed;
    public int target;
    public Animator animator;
    public bool isAlive;


    private void Start()
    {
        isAlive = true;
    }
    void Update()
    {
        if(isAlive)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[target].position, moveSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (transform.position == waypoints[target].position)
        {
            if (target == waypoints.Count -1)
            {
                target = 0;
            }
            else
            {
                target += 1;
            }
        }
    }

    public void Death()
    {
        isAlive = false;
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
     }
        StartCoroutine(DeathAnimation());
        
    }

    IEnumerator DeathAnimation()
    {
        animator.SetBool("Alive", false);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
