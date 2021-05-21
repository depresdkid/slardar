using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMove
{
    public void Moving()
    {
        print("јуе");
    }
    private void Start()
    {
        Moving();
    }
}
