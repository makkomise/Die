using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public Animator animator;
    public Rigidbody2D playerRB;
    public AudioSource audioSource;

    public bool grounded;
    public bool doubleJump;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public AudioClip walkSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundDetect();

        if (Input.GetButtonDown("Jump") && grounded)       //Hyppy
        {
            animator.SetTrigger("Jump");
            doubleJump = false;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Jump") && grounded == false && doubleJump == false)    //Tuplahyppy
        {
            animator.SetTrigger("Jump");
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            doubleJump = true;
        }

        if (playerRB.velocity.y < 0)   //Hyppyyn liittyvä funktio, hyppää korkeammalle mitä suurempi velocity pelaajalla on jne
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (playerRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Escape))     //Escistä takaisin Main Menuun
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void FixedUpdate()  //Hahmon liikkuminen x-akselilla
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
            
            if (!audioSource.isPlaying && grounded == true)     //Kävelyäänet päälle jos hahmo liikkuu eikä ole ilmassa
            {
                audioSource.clip = walkSound;
                audioSource.Play();
            }
        }
        else
        {
            animator.SetBool("Walk", false);            
        }
    }

    void GroundDetect()     //Tunnistaa onko pelaaja maassa, liittyy hyppelyyn
    {
        Vector3 boxPosition = transform.position - new Vector3(0, transform.localScale.y / 2, 0);
        RaycastHit2D rayhit = Physics2D.BoxCast(boxPosition, new Vector2(0.8f, 0.2f), 0, Vector2.zero, 0, LayerMask.GetMask("Ground"));

        if (rayhit)
        {
            grounded = true;
            animator.SetBool("Jump", false);
        }
        else 
        {
            grounded = false;
            animator.SetBool("Jump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)  //Kaikki asiat mistä pelaaja voi kuolla ":D"
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("DeathFloor"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("End");
        }             
    }
    
    public void Die()       //Itse kuolemisfunktio, siirtää oikeasti vaan pelaajan respawniin jos pelaaja osuu johonkin
    {
        playerRB.velocity = Vector3.zero;
        transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }
}
