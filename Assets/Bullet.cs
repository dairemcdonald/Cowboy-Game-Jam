using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int dir;
    public int speed;

    void Update()
    {
        transform.Translate(0, Time.deltaTime * dir * speed, 0);
    }

    public void Direction(int d)
    {
        dir = d;
    }

   
}
