using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int hp = 2;
    public int enemyAmount = 0;
    [SerializeField] float waveGap = 2f;
    [SerializeField] float spawnGap = 2f;
    private float timestamp;
    EnemySpawner[] Spawns;

    public GameObject PausePanel;
    private GameObject _pausePanel;

    public Dictionary<string, PlayerController> validKeys;
    public Canvas Canvas;
    private bool isGameOver = false;
    private void Awake()
    {
        if (FindObjectOfType<MusicPlayer>() == null)
        {
            Instantiate(Resources.Load(path: "MusicPlayer") as GameObject);
        }
        validKeys= new Dictionary<string, PlayerController>();
    }

    private void Start()
    {
        MusicPlayer.dialogueTracker = SceneManager.GetActiveScene().buildIndex;
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
            MusicPlayer.win = false;
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

        else if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Time.timeScale = 1;
        }

        else if (Input.anyKeyDown)
        {
            PlayerController player;
            validKeys.TryGetValue(Input.inputString.ToUpper(), out player);
            if (player != null)
            {
                player.ShootInput();
            }
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


    public void AddUnit(string key, PlayerController unit)
    {
        validKeys.Add(key, unit);
    }

    public void RemoveUnit(string key)
    {
        validKeys.Remove((key));
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
            MusicPlayer.win = true;
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

    

    void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }

    void ResumeLevel()
    {
        Destroy(_pausePanel);
        Time.timeScale = 1;
    }

    void MuteLevel()
    {
        MusicPlayer.Mute();
    }

    private void Pause()
    { 
       Time.timeScale = 0; 
        _pausePanel = Instantiate(PausePanel);
        _pausePanel.transform.Find("ResumeButton").GetComponent<Button>().onClick.AddListener(ResumeLevel);
        _pausePanel.transform.Find("MuteButton").GetComponent<Button>().onClick.AddListener(MuteLevel);
        _pausePanel.transform.Find("QuitButton").GetComponent<Button>().onClick.AddListener(QuitLevel);
        _pausePanel.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);
    }
}


