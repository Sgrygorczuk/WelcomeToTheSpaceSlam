using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //============================= External Scripts 
    Player playerScript;
    
    //============================= Varss 
    public float minSpeed;
    public float maxSpeed;
    public int damage;
    private float speed;
    public int type;
    public GameObject splat;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2.down instantly makes a Vector2(0, -1)
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D hitbox){
        
        if(hitbox.tag == "Player"){
            playerScript.damagePlayer(damage);

            if(type == 2 && !playerScript.isFrozen){
                playerScript.setFrozen(true);
            }

            if(type == 4 && !playerScript.isSlowed){
                playerScript.setSlowed(true);
            }

            Destroy(gameObject);
        }

        if(hitbox.tag == "Ground"){

            if(type == 4){
                Instantiate(splat, this.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
