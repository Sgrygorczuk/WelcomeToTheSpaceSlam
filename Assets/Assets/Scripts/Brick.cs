using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //================================= Timer 
    private float spawnTimer; //Timer
    public float SPAWN_TIME; //What timer resets to 

    //================================ Players 
    Player playerScript;
    Player playerScriptTwo;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = SPAWN_TIME; //Make sure to initialize otherwise it will disappear immediately 
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerScriptTwo = GameObject.FindGameObjectWithTag("Player Two").GetComponent<Player>();
    }

    // Update is called once per frame
    /**
    *Purpose: Updates ice brick if either of the players has it 
    */
    void Update()
    {
        timerUpdate(playerScript);
        timerUpdate(playerScriptTwo);
    }

    /**
    *Input: Player that
    *Purpose: Update given players timer if brick is spawend 
    */
    void timerUpdate(Player player){
        if(spawnTimer <= 0 && player.hasBeenFrozen){
            spawnTimer = SPAWN_TIME;
            player.isFrozen = false;
            player.hasBeenFrozen = false;
            Destroy(gameObject);
        }
        else{
            spawnTimer -= Time.deltaTime;
        }
    }
}
