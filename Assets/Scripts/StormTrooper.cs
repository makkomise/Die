﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTrooper : MonoBehaviour
{

    public float moveSpeed;
    public GameObject detectionPoint;
    public Animator animator;
    public Rigidbody2D trooperRB;
    public float direction = 1;
    public LayerMask groundLayer;

    public float fireRate;
    public float nextFire;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        trooperRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * direction, 0, 0);
        transform.localScale = new Vector3(direction, 1, 1);
        Fire();
    }

    private void LateUpdate()   //Liikkumisen funktio, vaihtaa suuntaa jos DetectionPoint osuu johonkin
    {
        RaycastHit2D hit = Physics2D.Raycast(detectionPoint.transform.position, Vector2.down, 1, groundLayer);

        if (hit.collider == null)
        {
            direction *= -1;
        }

        RaycastHit2D hit2 = Physics2D.Raycast(detectionPoint.transform.position, new Vector2(direction, 0), 0.1f, groundLayer);

        if (hit2.collider != null)
        {
            direction *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)     //Vaihtaa suuntaa osuessaan kaveriin
    {
        direction *= -1;
    }

    void Fire()     //Stormtrooperin ampuminen
    {
        if (Time.time > nextFire)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
