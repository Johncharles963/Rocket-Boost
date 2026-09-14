using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float sceneDelay = 2f;
    [SerializeField] AudioClip playerLanded;
    [SerializeField] AudioClip playerCrashed;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RespondToDebugKey();
    }

    private void RespondToDebugKey()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextScene();
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame) 
        {
            successParticles.Play();
            isCollidable = !isCollidable;
            Debug.Log("C key was pressed");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable){ return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into Friendly");
                break;
            case "Fuel":
                Debug.Log("Bumped into Fuel");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        audioSource.Stop();
        isControllable = false;
        audioSource.PlayOneShot(playerCrashed);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Debug.Log("Bumped into foreign object!");
        Invoke("ReloadLevel", sceneDelay);
        
    }

    void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        successParticles.Play();
        audioSource.PlayOneShot(playerLanded);
        GetComponent<Movement>().enabled = false;
        Debug.Log("Bumped into Finish");
        Invoke("LoadNextScene", sceneDelay);
    }

    void LoadNextScene()
    {
        int nextscene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextscene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextscene);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
