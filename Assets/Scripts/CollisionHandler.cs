using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip SuccessSound;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;    
    bool isTransitioning = false;
    bool collisionDisable = false;
    AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisable = !collisionDisable; // toggle 
        }
    }

    private void OnCollisionEnter(Collision other){

        if(isTransitioning || collisionDisable){return;}
        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("Friendly");
            break;
            case "Finish":
                StartSuccessScene();                  
            break;
            default:
                StartCrashScene();
            break;
        }
    }

    private void StartSuccessScene()
    {
            //todo sfx on success scene
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(SuccessSound);
            //todo particle effect  
            successParticle.Play();       
            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel",levelDelay);
    }

    void StartCrashScene(){
            //todo sfx on crash scene
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(crashSound);
            //todo particle effect 
            crashParticle.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel",levelDelay);
    }
    void ReloadLevel(){
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
    void LoadNextLevel(){
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentIndex+1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
