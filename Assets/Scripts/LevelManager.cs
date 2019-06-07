using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int hp = 2;
    [SerializeField] int waveLimit = 0;
    [SerializeField] int startLimit = 4;
    public int enemyAmount = 0;
    [SerializeField] int waveAmount= 4;
    [SerializeField] float timeGap = 2f;
    [SerializeField] float waveGap = 2f;
    [SerializeField] float waveMax = 5f;
    [SerializeField] float spawnGap = 2f;
    private float timestamp;
    [SerializeField] bool startWaveFinished = false;
    EnemySpawner[] Spawns;


    public Canvas Canvas;
    private bool isGameOver = false;

    private void Start()
    {
        int roundLimit = 2;
         int atTime = 1;
        spawnGap = 3f;
        StartCoroutine(startSpawn(roundLimit, atTime));
        

    }

    public void carriageDown()
    {
        hp--;
        if (hp <= 0 && !isGameOver)
        {
            FindObjectOfType<MusicPlayer>().win = false;
            isGameOver = true;
            LoadNextLevel();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Time.time >= timestamp)
        {
            waveLimit++;
            timestamp = Time.time + timeGap;
        }
        /*if (enemyAmount < waveLimit && enemyAmount < waveAmount && startWaveFinished)
        {
            newSpawn();
        }
        */
    }

    public void enemyDown()
    {
        enemyAmount--;
        waveAmount--;
        if (waveAmount <= 0)
        {
            isGameOver = true;
            FindObjectOfType<MusicPlayer>().win = true;
            Invoke("LoadNextLevel", 2f);
        }
           
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(4);
    }

    IEnumerator startSpawn(int roundLimit, int atTime)
    {
        for (int i = 0; i < roundLimit; i++)
        {
            for (int j = 0; j < atTime; j++)
            {

                wave2(atTime);
                yield return new WaitForSeconds(spawnGap);
            }
        }
        yield return new WaitForSeconds(waveGap);

        if (atTime <= 6)
        {
            Debug.Log("Recursion");
            for (int k = 0; k < Spawns.Length; k++)
            {
                Spawns[k].changeEnemySpeed();
            }

            spawnGap = spawnGap - 0.5f;
            StartCoroutine(startSpawn(roundLimit, atTime + 1));
        }
    }

    void newSpawn()
    {
        if (transform.childCount > 0)
        {

            Spawns = FindObjectsOfType<EnemySpawner>();
            System.Random random = new System.Random();
            int randomNumber = random.Next(0, Spawns.Length);
            EnemyMovement containsEnemy = null;
            try { containsEnemy = Spawns[randomNumber].GetComponentInChildren<EnemyMovement>(); }
            catch
            {
                Debug.Log("Enemy Spawn Checker Exception");
            }
            if (containsEnemy == null && Spawns[randomNumber] != null)
            {
                Spawns[randomNumber].spawnStarter();
                enemyAmount++;
                
            }
        }
        
    }

    void QSpawn()
    {
        if (transform.childCount > 0)
        {
                FindObjectOfType<EnemySpawner>().Spawn();
                enemyAmount++;
        }
     }

    void wave2(int atTime)
    {
        if (transform.childCount > 0)
        {
            Spawns = FindObjectsOfType<EnemySpawner>();
            System.Random random = new System.Random();
            int randomNumber = random.Next(0, Spawns.Length);
            EnemyMovement[] containsEnemy = null;
            try { containsEnemy = Spawns[randomNumber].GetComponentsInChildren<EnemyMovement>(); }
            catch
            {
                Debug.Log("Enemy Spawn Checker Exception");
            }
            if (containsEnemy.Length <= 2 && Spawns[randomNumber] != null)
            {
                Spawns[randomNumber].Spawn();
                enemyAmount++;
            }
            if (containsEnemy.Length > 0 && Spawns[randomNumber] != null)
            {
                StartCoroutine(spawnInvoke(randomNumber));
                

            }
        }
    }


    IEnumerator spawnInvoke(int randomNumber)
    {
        yield return new WaitForSeconds(1);
        Spawns[randomNumber].Spawn();
        enemyAmount++;
    }
    void LevelQuit()
    {
        SceneManager.LoadScene(0);
    }

    private void Pause()
    {
        if (Time.timeScale == 1)
        { Time.timeScale = 0; }
        else
        { Time.timeScale = 1; }

    }

}
