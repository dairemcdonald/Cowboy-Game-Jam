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
            Debug.Log("Hit: " + Time.time);
            //Debug.Log("Hit: " + collision.GetInstanceID() + "Time : " + Time.time);
            Destroy(collision.gameObject);
            FindObjectOfType<LevelManager>().enemyDown();
            Destroy(gameObject);
        }

    }
}
