using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int hp = 2;
    public int enemyAmount = 0;
    [SerializeField] int waveAmount= 4;
    [SerializeField] float timeGap = 2f;
    [SerializeField] float waveGap = 2f;
    [SerializeField] float spawnGap = 2f;
    private float timestamp;
    [SerializeField] bool startWaveFinished = false;
    EnemySpawner[] Spawns;


    public Canvas Canvas;
    private bool isGameOver = false;

    private void Start()
    {
        FindObjectOfType<MusicPlayer>().dialogueTracker = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            int roundLimit = 2;
            int atTime = 1;
            spawnGap = 3f;
            StartCoroutine(spawnLevelOne(roundLimit, atTime, 6));
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            int roundLimit = 2;
            int atTime = 4;
            spawnGap = 2f;
            StartCoroutine(spawnLevelOne(roundLimit, atTime, 7));
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            int roundLimit = 1;
            int atTime = 1;
            spawnGap = 3f;
            StartCoroutine(spawnLevelOne(roundLimit, atTime, 6));
        }


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
            LevelQuit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

    }

    public void enemyDown()
    {
        enemyAmount--;
           
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(4);
    }

    IEnumerator spawnLevelOne(int roundLimit, int atTime, int waveMax)
    {
        for (int i = 0; i < roundLimit; i++)
        {
            for (int j = 0; j < atTime; j++)
            {

                waveSpawn(atTime);
                yield return new WaitForSeconds(spawnGap);
            }
        }
        yield return new WaitForSeconds(waveGap);

        if (atTime <= waveMax)
        {
            Debug.Log("Recursion");
            for (int k = 0; k < Spawns.Length; k++)
            {
                Spawns[k].changeEnemySpeed(0.1f);
            }

            spawnGap = spawnGap - 0.5f;
            StartCoroutine(spawnLevelOne(roundLimit, atTime + 1, waveMax));
        }
        else
        {
            isGameOver = true;
            FindObjectOfType<MusicPlayer>().win = true;
            Invoke("LoadNextLevel", 2f);
        }
    }

  


    void waveSpawn(int atTime)
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
            if (Spawns[randomNumber] != null && containsEnemy.Length == 0 && FindObjectsOfType<EnemyMovement>().Length < atTime)
            {
                Spawns[randomNumber].Spawn();
                enemyAmount++;
            }
            if (containsEnemy.Length < 3 && containsEnemy.Length > 0 && Spawns[randomNumber] != null)
            {
                float enemySpeed = Spawns[randomNumber].getEnemySpeed();
                Spawns[randomNumber].delayedSpawn(6-enemySpeed);
                enemyAmount++;
            }
        }
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
