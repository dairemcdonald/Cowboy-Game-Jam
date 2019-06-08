using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    public int dialogueTracker;
    public bool win = false;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
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
            if (_audioSource.isPlaying)
            { StopMusic(); }
            else { PlayMusic(); }
        }

    }
    public void PlayMusic()
    {
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

}
