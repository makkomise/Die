using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropelHat : MonoBehaviour
{

    public float speed;
    public float angrySpeed;
    public float angryDistance;
    public float maxDistance;

    private Transform target;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (Vector2.Distance(transform.position, target.position) < maxDistance)
        {
            if (Vector2.Distance(transform.position, target.position) > angryDistance)
            {
                animator.SetBool("Angry", false);
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Angry", true);
                transform.position = Vector2.MoveTowards(transform.position, target.position, angrySpeed * Time.deltaTime);
            }
        }
    }
}
