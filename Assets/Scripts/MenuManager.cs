using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI textmesh;
    public GameObject but1, but2;
    public float wordDelay;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Skip();
        }
    }
        public void LoadFirstLevel()
    {
        but1.SetActive(false);
        but2.SetActive(false);
        textmesh.gameObject.SetActive(true);
        StartCoroutine("Dialogue"); 
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Skip()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    IEnumerator Dialogue()
    {
        textmesh.SetText("Well howdy there, partner! You must be our newest recruit");
        yield return new WaitForSeconds(wordDelay);
        textmesh.SetText("Okay, now see your job here is we have this train going 'cross country");
        yield return new WaitForSeconds(wordDelay);
        textmesh.SetText("This train is carrying a heck of a lot o' gold");
        yield return new WaitForSeconds(wordDelay);
        textmesh.SetText("So every bandit will come-a-calling");
        yield return new WaitForSeconds(wordDelay);
        textmesh.SetText("That's where you come in. We need you to protect the train.");
        yield return new WaitForSeconds(wordDelay);
        textmesh.SetText("Every one of our fellers can be hollared to shoot by a letter");
        yield return new WaitForSeconds(wordDelay);
        textmesh.SetText("You'll notice those letters match up with the layout of your telegraph machine");
        yield return new WaitForSeconds(wordDelay);
        textmesh.SetText("So keep hollaring those letters and keeping the train a-going");
        yield return new WaitForSeconds(wordDelay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
