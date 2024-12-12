using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    Movement movement;

    bool isControllable = true;

    [Header("Audio Reference Settings")]
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [Header("Delay Settings")]
    [SerializeField] float delay = 2f;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }
    void OnCollisionEnter(Collision other) 
    {
        if (!isControllable) { return; }

        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("This is safe to land!");
                break;
            case "Fuel":
                Debug.Log("You have grabbed fuel!");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void StartNextLevelSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delay);
    }

    void NextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        
        if (nextScene == SceneManager.sceneCountInBuildSettings )
        {
            nextScene = 0;
        }
        
        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
