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

    public void delayedSpawn(float enemyGap)
    {
        StartCoroutine(resumeSpawn(enemyGap));
    }

    IEnumerator resumeSpawn(float enemyGap)
    {
        var tempObj = Instantiate(prefab, transform.position, Quaternion.identity);
        tempObj.transform.parent = this.transform;
        tempObj.delayed = true;
        yield return new WaitForSeconds(enemyGap);
        tempObj.delayed = false;
    }

    public void changeEnemySpeed(float change)
    {
        prefab.GetComponent<EnemyMovement>().speed += change; 
    }

    public float getEnemySpeed()
    {
        return prefab.GetComponent<EnemyMovement>().speed;
    }
}
