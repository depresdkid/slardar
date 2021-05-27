using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource audio;
    Rigidbody2D rigidbody;    
    SpriteRenderer sprite;
    GameObject _gameObject;
    public float speed;
    public int damage;
    void End() {
        Destroy(gameObject);
    }
    private void Awake()
    {
        _gameObject = gameObject;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * speed;
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.player.GetDamage(damage);
            audio.Play();
            sprite.enabled = false;
            Invoke("End",1f);
            
        }
    }
    void Update()
    {
        
    }
}
