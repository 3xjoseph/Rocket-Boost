using UnityEngine;

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
                 audioSource.PlayOneShot(finishLevel);
                break;
            default:
                audioSource.PlayOneShot(crash);
                Debug.Log("You have crashed!");
                break;
        }
    }
}
