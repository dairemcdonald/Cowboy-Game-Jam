using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 5f;
    public EnemyMovement prefab;

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
        spawnDelay = random.Next(0, 10);
        Invoke("Spawn", spawnDelay);
    }

    private void Spawn()
    {
        var tempInt = Instantiate(prefab, transform.position, Quaternion.identity);
        tempInt.transform.parent = this.transform;
        //Debug.Log("Spawn: " + tempInt.GetInstanceID() + "Time : "+ Time.time);
    }
}
