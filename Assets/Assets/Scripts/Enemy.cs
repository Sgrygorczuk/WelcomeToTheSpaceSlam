using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //============================= External Scripts 
    Player playerScript;
    Player playerScriptTwo;
    
    //============================= Vars
    public float minSpeed;  //Min speed the ball can fly 
    public float maxSpeed;  //Max speed the ball can fly 
    private float speed;    //Speed of ball 
    public int type;        //Which ball it is 
    public GameObject splat; //Splat object that will spawn if slime ball hits ground 
    public GameObject FX;   //Particle Effect 

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
        
        //Updates player collision 
        if(hitbox.tag == "Player"){
            updatePlayerCollision(playerScript);
        }
        if(hitbox.tag == "Player Two"){
            updatePlayerCollision(playerScriptTwo);
        }

        //Update collision with ground 
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

    /**
    * Input: player 
    * Purpose: Update collision between player and diffrent ball types 
    */
    void updatePlayerCollision(Player player){
        //Regular ball 
        if(type == 0){
            player.setIsHoldingBall(true);
        }
        //Fire ball 
        else if(type == 1 && !player.isSlowed &&  !player.isSpeedy && !player.isImmune ){
            player.setIsSpeedy(true);
        }
        //Ice ball 
        else if(type == 2 && !player.isFrozen && !player.isImmune){
            player.isFrozen = true;
        }
        //Rainbow ball 
        else if(type == 3 && !player.isImmune){
            player.isImmune = true;
            player.trailRenderer.time = 1;
        }
        //Slime Ball 
        else if(type == 4 && !player.isSlowed &&  !player.isSpeedy && !player.isImmune){
            player.setIsSlowed(true);
        }
       
        //Make particle FX and destory ball 
        Instantiate(FX, player.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
