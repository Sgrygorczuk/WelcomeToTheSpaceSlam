using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    //========================= Text 
    public GameObject text; //Credits text 

    // Update is called once per frame
    /**
    *Purpose: Check for user Key press or text being off the screen 
    */
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape) || text.transform.position.y > -4){
            SceneManager.LoadScene("MainMenu");
        }
    }
}
