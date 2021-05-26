using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IArounds
{
    [SerializeField] int damage;
    [SerializeField] float reloadTime;
    public AudioSource audio;
    Animator animator;
    float timer = 0;
    bool isAttack = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void AroundsActions()
    {
        Player.player.GetDamage(damage);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && !isAttack && Player.player.isAlive)
        {
            animator.SetBool("IsAttack", true);            
            audio.Play();
            AroundsActions();
            timer = reloadTime;
            
            isAttack = true;
        }
    }
    private void Update()
    {
        if (timer <= 0)
        {
            animator.SetBool("IsAttack", false);
            isAttack = false;            
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
