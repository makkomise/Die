using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
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


    void Update()       //Kärpäsen lentofunktio, satunnaisgeneroitu lentely ympäriinsä
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft += accelerationTime;
        }
    }

    void FixedUpdate()  //kärbän lentonopeus
    {
        flyRB.AddForce(movement * maxSpeed);
    }
    
}
