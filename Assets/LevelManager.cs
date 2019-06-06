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
    [SerializeField] float startGap = 2f;
    private float timestamp;
    [SerializeField] bool startWaveFinished = false;
    EnemySpawner[] Spawns;


    public Canvas Canvas;
    private bool isGameOver = false;

    private void Start()
    {
       
            StartCoroutine("startSpawn");
            FindObjectOfType<MusicPlayer>().dialogueTracker = SceneManager.GetActiveScene().buildIndex;

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
        if (enemyAmount < waveLimit && enemyAmount < waveAmount && startWaveFinished)
        {
            newSpawn();
        }
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

    IEnumerator startSpawn()
    {
        for (int i = 0; i < startLimit; i++)
        {
            newSpawn();
            yield return new WaitForSeconds(startGap);
        }
        startWaveFinished = true;
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
