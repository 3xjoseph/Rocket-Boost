using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    Movement movement;

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
        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("This is safe to land!");
                break;
            case "Fuel":
                Debug.Log("You have grabbed fuel!");
                break;
            case "Finish":
                Debug.Log("You have finished the level!");
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                Debug.Log("You have crashed!");
                break;
        }
    }

    void StartCrashSequence()
    {
        audioSource.PlayOneShot(crash);

        DisableMovement();

        Invoke("ReloadLevel", delay);
    }

    void DisableMovement()
    {
        movement.thrust.Disable();
        movement.rotation.Disable();
    }

    void StartNextLevelSequence()
    {
        audioSource.PlayOneShot(success);
        DisableMovement();
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

    private void ReloadLevel()
    {
        
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
