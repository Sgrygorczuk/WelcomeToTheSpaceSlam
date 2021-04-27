using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    private float spawnTimer;
    public float SPAWN_TIME;

    Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = SPAWN_TIME;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer <= 0 && playerScript.hasBeenFrozen){
            spawnTimer = SPAWN_TIME;
            playerScript.isFrozen = false;
            playerScript.hasBeenFrozen = false;
            Destroy(gameObject);
        }
        else{
            spawnTimer -= Time.deltaTime;
        }
    }
}
