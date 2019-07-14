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
        previousLevel = MusicPlayer.dialogueTracker;
        win = MusicPlayer.win;
        string dialogue = "";
        if (previousLevel != 3 && win == true)
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
         {
            yield return new WaitForSeconds(wordDelay);
            SceneManager.LoadScene(previousLevel + 1);
        }
        else if (previousLevel == 3 && win)
        {
            Debug.Log("Win Interlude");
            yield return new WaitForSeconds(endWordDelay);
            SceneManager.LoadScene(0);
        }

        else
        {
            Debug.Log("End Interlude");
            yield return new WaitForSeconds(wordDelay);
            SceneManager.LoadScene(previousLevel); }

    }

}
