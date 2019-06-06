using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterludeManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textmesh;
    public float wordDelay;
    public float endWordDelay;
    private int previousLevel;
    private bool win = false;

    private void Start()
    {
        previousLevel = FindObjectOfType<MusicPlayer>().dialogueTracker;
        win = FindObjectOfType<MusicPlayer>().win;
        string dialogue = "";
        if (previousLevel != 3 && win)
        { dialogue = "Level " + (previousLevel + 1) ; }
        else if (previousLevel == 3 && win)
        { dialogue = "Well done buckaroo! The train has made it safe and sound thanks to you"; }
        else 
        { dialogue = "Hard luck, partner! Better try again next time!";  
        }

        StartCoroutine("Dialogue", dialogue);
    }
    IEnumerator Dialogue(string dialogueParams)
    {
        textmesh.SetText(dialogueParams);
        if (previousLevel != 3 && win)
            { yield return new WaitForSeconds(wordDelay); }
        if (previousLevel == 3 && win)
        { yield return new WaitForSeconds(endWordDelay); }

        if (previousLevel != 3 && win)
            {
            SceneManager.LoadScene(previousLevel + 1);
            }
        else if (previousLevel == 3 && win)
        {
            SceneManager.LoadScene(0);
        }
        else
        { SceneManager.LoadScene(previousLevel); }

    }

}
