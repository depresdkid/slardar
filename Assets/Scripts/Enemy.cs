using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _health;
    Animator animator;
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
    protected abstract void Attack();
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected void GetDamage(float damage) {
        _health -= damage;
    }
}
