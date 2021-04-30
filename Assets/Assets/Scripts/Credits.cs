using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    public GameObject text;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape) || text.transform.position.y > -4){
            SceneManager.LoadScene("MainMenu");
        }
    }
}
