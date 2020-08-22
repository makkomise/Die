using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float projectileSpeed;
    Rigidbody2D projectileRB;
    private Transform target;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        projectileRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moveDirection = (target.transform.position - transform.position).normalized * projectileSpeed;
        projectileRB.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 1);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}