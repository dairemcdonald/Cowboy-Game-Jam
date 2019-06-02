using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int dir;

    void Update()
    {
        transform.Translate(0, Time.deltaTime * dir, 0);
    }

    public void Direction(int d)
    {
        dir = d;
    }

   
}
