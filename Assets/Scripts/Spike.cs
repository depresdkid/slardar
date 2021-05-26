using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IArounds
{
    [SerializeField] int damage;
    [SerializeField] float reloadTime;
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
        print("Стою");
        if (collision.gameObject.tag == "Player" && !isAttack)
        {
            isAttack = true;
            animator.SetTrigger("IsDamage");
            AroundsActions();
            timer = reloadTime;
        }
    }
    private void Update()
    {
        print(isAttack);
        if (timer <= 0)
        {
            isAttack = false;            
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
