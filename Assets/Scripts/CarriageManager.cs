using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            FindObjectOfType<LevelManager>().carriageDown();
            Destroy(collision.gameObject);
            FindObjectOfType<LevelManager>().enemyDown();
            Destroy(gameObject);
        }

    }
}
