using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected AudioSource _audioRun,_audioHit, _audioScrimer;
    [SerializeField] protected float _health;
    [SerializeField] protected int _damage;
    protected Animator animator;
    protected GameObject gameObgectEnemy;
    bool isDead = false;
    //Свойство (жив или нет)
    protected bool IsAlive
    {
        get
        {
            if (_health > 0)
            {                
                return true;                
            }
            else
            {
                return false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag == "HitBlock" && _health>0)
        {
            animator.SetTrigger("GetDamage");
            GetDamage(Player.player.damage);
        }
    }
    protected abstract void Attack();
    public virtual void Start()
    {
        gameObgectEnemy = gameObject;
        animator = GetComponent<Animator>();
    }
    protected void GetDamage(float damage) {
        if (damage>0)
        {
            _health -= damage;
        }        
    }
    private void Update()
    {
        //срабатывает анимация смерти в случае если hp меньше 0 или равны
        if (!IsAlive && !isDead)
        {
            animator.SetTrigger("Die");
            isDead = true;
        }
    }
}
