using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //================================= Objects 
    public Transform[] spawnPoints; //Will hold the Postion details of the spawnPoints
    public GameObject[] hazards; //Holds enemy types collected from PreFabs that were spawned 

    //============================== Timers 
    private float spawnTime;   //The timer
    public float startSpawn;   //Max time the timer can be a
    public float MIN_TIME;     //The min time that timer can be set to 
    public float decreaseTime; //The amount the timer will be lowered by till it hits MIN_TIME
        
    //========================= Flags 
    public bool basketballSpawner;  //Tells us if we're spawning regular balls or all the hazards 

    //============================ Code 

    void Start()
    {
        //No intilizing timer cuz we want it to start off immediately 
    }

    // Update is called once per frame
    /**
    *Purpose: Count down and spawn a ball 
    */
    void Update()
    {
        if(spawnTime <= 0){
            //Grabs the random location where we're going to spawn it 
             int spawnPoint = Random.Range(0, spawnPoints.Length);
            Transform randomSpawnPoint = spawnPoints[spawnPoint]; 
            //Gets the type of hazard we're spawning 
            GameObject randomHazard;
            randomHazard = hazards[Random.Range(1, hazards.Length)];
            
            if(basketballSpawner){
                Instantiate(hazards[0], randomSpawnPoint.position, Quaternion.identity);
            }
            else{
                Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.identity);
            }

            //Makes the speed of spawning faster as the game goes on 
            if(startSpawn >= MIN_TIME){
                startSpawn -= decreaseTime;
            }

            //Resets the spawn timer 
             spawnTime = startSpawn; 
        }
        else{
            //Updates the spawn timer 
             spawnTime -= Time.deltaTime;
        }
        
    }
}
