using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip finishLevel;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
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
                NextLevel();
                audioSource.PlayOneShot(finishLevel);
                break;
            default:
                ReloadLevel();
                Debug.Log("You have crashed!");
                break;
        }
    }

    private static void NextLevel()
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
        audioSource.PlayOneShot(crash);
        
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
