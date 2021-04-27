using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //============================= External Scripts 
    Player playerScript;

    public bool playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitbox){
        
        if(hitbox.tag == "Player" && playerScript.isHoldingBall && playerGoal){
            playerScript.setIsHoldingBall(false);
        }

    }
}
