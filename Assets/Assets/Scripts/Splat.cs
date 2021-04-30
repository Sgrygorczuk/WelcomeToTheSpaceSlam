using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{

    private float spawnTime;
    public float startSpawn;
    Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = startSpawn;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
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


    void OnTriggerEnter2D(Collider2D hitbox){
        
        if(hitbox.tag == "Player" && (playerScript.getInput() == -1 || playerScript.getInput() == 1)){ 
            playerScript.isTripped = true;
        }
    }
}
