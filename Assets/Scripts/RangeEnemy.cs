using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    public AudioSource audio;
    public GameObject bullet;
    public void AttackRange()
    {
        if (Player.player.isAlive)
        {
            audio.Play();            
        }
    }

}
