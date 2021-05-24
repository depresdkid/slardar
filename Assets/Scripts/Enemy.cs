using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected AudioSource _audioRun,_audioHit;
    [SerializeField] protected float _health;
    [SerializeField] protected int _damage;
    protected Animator animator;
    protected GameObject gameObgectEnemy;
    bool isDead = false;
    //�������� (��� ��� ���)
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
        gameObgectEnemy = gameObject;
        animator = GetComponent<Animator>();
    }
    protected void GetDamage(float damage) {
        _health -= damage;
    }
    private void Update()
    {
        //����������� �������� ������ � ������ ���� hp ������ 0 ��� �����
        if (!IsAlive && !isDead)
        {
            animator.SetTrigger("Die");
            isDead = true;
        }
    }
}
