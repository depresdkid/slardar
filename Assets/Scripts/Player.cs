using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMove
{
    [SerializeField] private float speed;
    [SerializeField] private float lives;
    [SerializeField] private float jumpForse;
    private Rigidbody2D rbPlayer;
    private SpriteRenderer spritePlayer;
    public bool isReady = true;
    public int doubleJump = 1;
    private bool isLeft = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        isReady = true;
        doubleJump = 1;

    }
    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        spritePlayer = GetComponent<SpriteRenderer>();


    }

    void playerMove()
    {
        float velosity = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * velosity * speed * Time.deltaTime);
        if (isLeft)
        {
            spritePlayer.flipX = true;
        }
        else {
            spritePlayer.flipX = false;
        }
        if (velosity > 0)
        {
            isLeft = false;

        }
        else if (velosity < 0)
        {
            isLeft = true;

        }

    }
    void playerJump() {

        rbPlayer.AddForce(transform.up * jumpForse, ForceMode2D.Impulse);
        
    }
    public void Moving()
    {
        playerMove();
    }   
    
    private void FixedUpdate()
    {

        Moving();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isReady)
        {
            playerJump();
            doubleJump++;
            if (doubleJump < 3)
            {
                isReady = true;
            }
            else {
                isReady = false;
            }
        }
    }
   
}
