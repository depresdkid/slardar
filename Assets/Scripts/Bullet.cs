using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    System.Random random = new System.Random();

    public AudioSource audio;
        
    Rigidbody2D rigidbody;    
    SpriteRenderer sprite;
    Transform player;

    public float speed;
    public int damage;

    bool _isHit = false;
    
    private void Awake()
    {
        double rand = random.NextDouble();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        rigidbody = GetComponent<Rigidbody2D>();
        
        sprite = GetComponent<SpriteRenderer>();

        rigidbody.velocity = transform.right * speed;
        if (player.position.x < transform.position.x)
        {
            rigidbody.velocity = new Vector2(-speed, (float)rand);
        }        
        else
            rigidbody.velocity = new Vector2(speed, (float)rand);
        try
        {
            Invoke("End", 4f);
        }
        catch (System.Exception)
        {
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !_isHit)
        {
            _isHit = true;
            Player.player.GetDamage(damage);
            audio.Play();
            sprite.enabled = false;
            Invoke("End",1f);
            
        }
    }
    void End()
    {
        Destroy(gameObject);
    }
}
