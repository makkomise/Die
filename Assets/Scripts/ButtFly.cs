using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtFly : MonoBehaviour
{

    public float accelerationTime = 1f;
    public float maxSpeed = 5f;
    private Vector2 movement;
    private float timeLeft;
    public Rigidbody2D flyRB;

    void Start()
    {
        flyRB = GetComponent<Rigidbody2D>();
    }


    void Update()   //Perhosen lentelyjutut, randomgeneroituu
    {
        timeLeft -= Time.deltaTime;     
        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft += accelerationTime;
        }

        if (Vector3.Distance(transform.position, flyRB.position) > 1f)
        {
            transform.Translate(new Vector3(maxSpeed * Time.deltaTime, 0, 0));
        }
    }

    void FixedUpdate()  //Perhosen lentonopeusjuttu
    {
        flyRB.AddForce(movement * maxSpeed);
    }

}
