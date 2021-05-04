using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //============================== Audio     
    AudioSource source; //The audio that is connected to button presses 

    //=============================== Code

    /**
    *Purpose: Add the audio component to the button 
    */
    void Start(){
        source = GetComponent<AudioSource>();
    }

    /**
    *Purpose: Send us to the Single Player Scene  
    */
    public void startSinglePlayer(){
        source.Play();
        SceneManager.LoadScene("SinglePlayer");
    }

    /**
    *Purpose: Send us to the Two Player Scene 
    */
    public void startTwoPlayer(){
         source.Play();
        SceneManager.LoadScene("TwoPlayer");
    }


    /**
    *Purpose: Send us to the Credits Scene 
    */
    public void startCredits(){
        source.Play();
        SceneManager.LoadScene("Credits");
    }

    /**
    *Purpose: Send us to the Main Menu Scene 
    */
     public void startMainMenu(){
        source.Play();
        SceneManager.LoadScene("MainMenu");
    }

}
