using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RangeEnemy : MonoBehaviour
{
    public AudioSource audio;
    public GameObject bullet;
    public Transform bulletSpawner;
    public void AttackRange()
    {
        if (Player.player.isAlive)
        {
            Instantiate(bullet, bulletSpawner.position, bulletSpawner.rotation);
            audio.Play();    
        }
    }
}
