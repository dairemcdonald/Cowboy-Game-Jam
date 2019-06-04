using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int hp = 2;
    [SerializeField] int waveLimit = 0;
    [SerializeField] int startLimit = 4;
    public int enemyAmount = 0;
    [SerializeField] int waveAmount= 4;
    [SerializeField] float timeGap;
    [SerializeField] float startGap;
    private float timestamp;
    [SerializeField] bool startWaveFinished = false;


    public GameObject gameOverText;
    private GameObject _gameoverText;

    public Canvas Canvas;
    private bool isGameOver = false;

    private void Start()
    {
       
            StartCoroutine("startSpawn");
     
    }

    public void carriageDown()
    {
        hp--;
        if (hp <= 0 && !isGameOver)
        {
            _gameoverText = Instantiate(gameOverText, transform.position, Quaternion.identity);
            _gameoverText.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);
            isGameOver = true;

        }
    }

    private void Update()
    {
       
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
            _gameoverText = Instantiate(gameOverText, transform.position, Quaternion.identity);
            _gameoverText.transform.GetComponent<Text>().text = "You win Partner!";
            _gameoverText.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);
            isGameOver = true;
        }
           
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
        
        var Spawns = FindObjectsOfType<EnemySpawner>();
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, Spawns.Length);
        var containsEnemy = Spawns[randomNumber].GetComponentInChildren<EnemyMovement>();
        if(containsEnemy == null)
        {
            Spawns[randomNumber].spawnStarter();
            enemyAmount++;
        }
        else
        {
            newSpawn();
        }
    }

}
