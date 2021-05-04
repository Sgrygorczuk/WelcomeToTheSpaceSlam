using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*Splat class creates the game object after the slime ball hits the ground 
    the player(s) can walk into the slime and trip over it, it dies after a timer 
*/
public class Splat : MonoBehaviour
{
    //=============================== Time Vars
    private float spawnTime;    //Timer 
    public float startSpawn;    //What timer resets to

    //=============================== Player reference 
    Player playerScript;    //Grabs the script of player one to trip them
    Player playerScript2;   //Grabs the script of player two to trip them

    // Start is called before the first frame update
    /**
    *Purpose: Initialize the time and player scripts 
    */
    void Start()
    {
        spawnTime = startSpawn; //Make sure to initialize otherwise it will disappear immediately 
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerScript2 = GameObject.FindGameObjectWithTag("Player Two").GetComponent<Player>();
    }

    // Update is called once per frame
    /**
    *Purpose: Count down till the object is destroyed 
    */
    void Update()
    {
        if(spawnTime <= 0){
            spawnTime = startSpawn;

            Destroy(gameObject);
        }
        else{
            spawnTime -= Time.deltaTime;
        }
    }

    /**
    *Input: hidBox
    *Purpose: Check for any hitbox collision if it occurs set the player that collided with it into a trip state 
    */
    void OnTriggerEnter2D(Collider2D hitbox){
        
        if(hitbox.tag == "Player" && (playerScript.getInput() == -1 || playerScript.getInput() == 1)){ 
            playerScript.isTripped = true;
        }
        if(hitbox.tag == "Player Two" && (playerScript2.getInput() == -1 || playerScript2.getInput() == 1)){ 
            playerScript2.isTripped = true;
        }
    }
}
