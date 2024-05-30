using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip sceneMusic;
    private AudioSource audioSource;
    public string sceneName; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            PlayMusic();
        }
    }

    void OnEnable()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name == sceneName)
        {
            PlayMusic();
        }
        else
        {
            StopMusic();
        }
    }

    void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = sceneMusic;
            audioSource.Play();
        }
    }

    void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

