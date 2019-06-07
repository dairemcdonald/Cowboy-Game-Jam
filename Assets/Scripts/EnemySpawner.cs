using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int spawnDelay = 3;
    public EnemyMovement prefab;

    private void Start()
    {
        prefab.GetComponent<EnemyMovement>().speed = 4.5f;
    }
    private void Update()
    {
        
            if (GetComponentsInChildren<EnemyMovement>().Length > 1)
            {
            Debug.Log("Extra Object: " + GetComponentsInChildren<EnemyMovement>()[1].gameObject.name);
            Destroy(GetComponentsInChildren<EnemyMovement>()[1].gameObject);
                FindObjectOfType<LevelManager>().enemyAmount--;
            
        }
    }

    public void spawnStarter()
    {
        System.Random random = new System.Random();
        float randSpawn = random.Next(0, spawnDelay);
        Invoke("Spawn", randSpawn);
    }

    public void Spawn()
    {
        var tempInt = Instantiate(prefab, transform.position, Quaternion.identity);
        tempInt.transform.parent = this.transform;
        //Debug.Log("Spawn: " + tempInt.GetComponentInParent<EnemySpawner>().gameObject.name + " Time : "+ Time.time);
    }

    public void changeEnemySpeed()
    {
        prefab.GetComponent<EnemyMovement>().speed += 0.1f; 
    }
}
