using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    Movement movement;

    bool isControllable = true;

    [Header("Audio Reference Settings")]
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;


    [Header("Particle System Reference Settings")]
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

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
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        
        Transform firstChild = transform.GetChild(0);
        // Get the GameObject of the child
        GameObject childObject = firstChild.gameObject;
        
        childObject.SetActive(false);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void StartNextLevelSequence()
    {
        isControllable = false;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
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
