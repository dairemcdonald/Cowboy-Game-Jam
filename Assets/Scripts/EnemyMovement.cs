using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int enemyDirection;
    public float speed = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, Time.deltaTime * enemyDirection * speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            FindObjectOfType<LevelManager>().enemyDown();
            Destroy(gameObject);
        }

        
    }


}
