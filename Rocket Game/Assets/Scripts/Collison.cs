using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Collison : MonoBehaviour
{

    [SerializeField] float waitForDead = 1f;
    [SerializeField] AudioClip missionSFX;
    [SerializeField] AudioClip boomSFX;
    [SerializeField] ParticleSystem expolision;
    [SerializeField] ParticleSystem sucsessParticle;

    AudioSource audioSource;

    bool isVoiceClose=true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        BugDebugging();
    }
    void BugDebugging()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame) // wasPressedThisFrame =>  Sadece bir kez true d�nd�r�r
        {
            LoadNextLevel();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        

        if (!isVoiceClose)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("nice");
                break;
            case "Finish":
                
                StartTheMission();

                break;
            default:
                StartTheDead();
                break;
        }
    }

    private void StartTheMission()
    {
          
        isVoiceClose = false;
        audioSource.Stop();
        audioSource.PlayOneShot(missionSFX);
        sucsessParticle.Play();
        GetComponent<Mover>().enabled = false;
        Invoke("LoadNextLevel", waitForDead);
    }

    void StartTheDead()
    {
        isVoiceClose = false;
        audioSource.Stop(); 
        audioSource.PlayOneShot(boomSFX);
        expolision.Play();
        GetComponent<Mover>().enabled = false; // bir yere �arp�nca mover scriptini devre d��� b�rak.
        Invoke("ReloadLevel", waitForDead);

    }
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;

        }

        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    { // Bu kod, oyunu s�f�rlamak, b�l�m� yeniden ba�latmak veya yeniden do�ma (respawn) sistemi gibi i�lemler i�in kullan�labilir.
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);


    }





}
