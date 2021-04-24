using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
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
        if(spawnTime <= 0 && playerScript.getHasBeenFrozen()){
            spawnTime = startSpawn;
            playerScript.setFrozen(false);
            playerScript.setHasBeenFrozen(false);
            Destroy(gameObject);
        }
        else{
            spawnTime -= Time.deltaTime;
        }
    }
}
