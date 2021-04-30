using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    AudioSource source;

    void Start(){
        source = GetComponent<AudioSource>();
    }

    public void startSinglePlayer(){
        source.Play();
        SceneManager.LoadScene("SinglePlayer");
    }

    public void startTwoPlayer(){
         source.Play();
        SceneManager.LoadScene("TwoPlayer");
    }

    public void startCredits(){
        source.Play();
        SceneManager.LoadScene("Credits");
    }

     public void startMainMenu(){
        source.Play();
        SceneManager.LoadScene("MainMenu");
    }

}
