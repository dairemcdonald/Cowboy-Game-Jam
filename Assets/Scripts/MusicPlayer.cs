using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private static MusicPlayer _instance;
    public static int dialogueTracker;
    public static bool win = false;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Assert.IsNull(_instance);
        _instance = this;
        _audioSource = GetComponent<AudioSource>();

        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Mute();
        }
    }

    private void mute()
    {
        if (_audioSource.isPlaying)
        { StopMusic(); }
        else { PlayMusic(); }
    }

    private void playMusic()
    {
        _audioSource.Play();
    }

    private void stopMusic()
    {
        _audioSource.Stop();
    }

    #region Public Members



    public static void Mute()
    {
        _instance.mute();
    }

    private void PlayMusic()
    {
        _instance.playMusic();
    }

    private void StopMusic()
    {
        _instance.stopMusic();
    }
    #endregion
}
