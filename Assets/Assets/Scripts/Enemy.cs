using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //============================= External Scripts 
    Player playerScript;
    Player playerScriptTwo;
    
    //============================= Vars
    public float minSpeed;
    public float maxSpeed;
    public int damage;
    private float speed;
    public int type;
    public GameObject splat;
    public GameObject FX;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if( GameObject.FindGameObjectWithTag("Player Two").GetComponent<Player>() != null){
            playerScriptTwo = GameObject.FindGameObjectWithTag("Player Two").GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2.down instantly makes a Vector2(0, -1)
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D hitbox){
        
        if(hitbox.tag == "Player"){

            if(type == 0){
                playerScript.setIsHoldingBall(true);
            }
            else if(type == 1 && !playerScript.isSlowed &&  !playerScript.isSpeedy && !playerScript.isImmune ){
                playerScript.setIsSpeedy(true);
            }
            else if(type == 2 && !playerScript.isFrozen && !playerScript.isImmune){
                playerScript.isFrozen = true;
            }
            else if(type == 3 && !playerScript.isImmune){
                playerScript.isImmune = true;
                playerScript.trailRenderer.time = 1;
            }
            else if(type == 4 && !playerScript.isSlowed &&  !playerScript.isSpeedy && !playerScript.isImmune){
                playerScript.setIsSlowed(true);
            }

            Instantiate(FX, playerScript.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        if(hitbox.tag == "Player Two"){

            if(type == 0){
                playerScriptTwo.setIsHoldingBall(true);
            }
            else if(type == 1 && !playerScriptTwo.isSlowed &&  !playerScriptTwo.isSpeedy && !playerScriptTwo.isImmune ){
                playerScriptTwo.setIsSpeedy(true);
            }
            else if(type == 2 && !playerScriptTwo.isFrozen && !playerScriptTwo.isImmune){
                playerScriptTwo.isFrozen = true;
            }
            else if(type == 3 && !playerScript.isImmune){
                playerScriptTwo.isImmune = true;
                playerScriptTwo.trailRenderer.time = 1;
            }
            else if(type == 4 && !playerScriptTwo.isSlowed &&  !playerScriptTwo.isSpeedy && !playerScriptTwo.isImmune){
                playerScriptTwo.setIsSlowed(true);
            }

            Instantiate(FX, playerScriptTwo.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        if(hitbox.tag == "Ground"){

            if(type == 4){
                Instantiate(splat, this.transform.position, Quaternion.identity);
            }
            else{
                Instantiate(FX, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
