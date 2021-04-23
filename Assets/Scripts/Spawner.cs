using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoints; //Will hold the Postion details of the spawnPoints
    public GameObject[] hazards; //Holds enemy types collected from PreFabs that were spawned 

    private float spawnTime;
    public float startSpawn;
    public float MIN_TIME;
    public float decreaseTime; 

    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        //Makes sure it only spawns stuff while we're player exits 
        if(player != null){

                if(spawnTime <= 0){
                //Grabs the random location where we're going to spawn it 
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; 
                //Gets the type of hazard we're spawning 
                GameObject randomHazard = hazards[Random.Range(0, hazards.Length)];

                //Creates the object 
                Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.identity);

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
}
