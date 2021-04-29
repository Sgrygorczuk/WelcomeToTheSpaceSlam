using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void startSinglePlayer(){
        SceneManager.LoadScene("SinglePlayer");
    }

    public void startTwoPlayer(){
        SceneManager.LoadScene("TwoPlayer");
    }

    public void startCredits(){
        SceneManager.LoadScene("Credits");
    }

     public void startMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

}
