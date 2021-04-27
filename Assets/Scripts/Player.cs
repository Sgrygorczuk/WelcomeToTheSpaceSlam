using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    //============================== Vars 
    private float input = 0;
    public float speed; 
    public int health;

    //========================== Frozen 
    public bool isFrozen;
    public bool hasBeenFrozen;

    //============================ Slow Down
    private float slowTimer;
    public float SLOW_TIME;
    public bool isSlowed;
    private float speedSlow = 1;

    //============================== Trip 
    private float trippedTimer;
    public float TRIPPED_TIME;
    public bool isTripped;
    private float zAngle;

    //========================= Holding Ball 
    public bool isHoldingBall;

    //============================= Referanced Object Traits 
    public GameObject frozenBrick;
    Rigidbody2D rigidbody;
    Animator animator; 

    //============================== Sprite
    public SpriteRenderer head;
    public SpriteRenderer body;
    public SpriteRenderer leftArm;
    public SpriteRenderer rightArm;
    public SpriteRenderer leftLeg;
    public SpriteRenderer rightLeg;
    public SpriteRenderer ball;

    private bool ballVisiblity = false;

    // Start is called before the first frame update
    void Start()
    {
        slowTimer = SLOW_TIME;
        animator = GetComponent<Animator>();     //Looks for the animatior object connected to the player 
        rigidbody = GetComponent<Rigidbody2D>(); //Looks if the object has a RidgeBody2D component it can connect to 
        ball.color = new Color (0, 0, 0, 0); 
    }

    // Update is called once per frame
    //Used for everything else 
    void Update()
    {
        spriteUpdates(); 
        slowedUpdates();
        frozenUpdates();
        trippedUpdates();
    }

    /*
    * Purpose: Has a timer that ticks down till the player is no longer slowed 
    */
    private void slowedUpdates(){
        if(isSlowed){
            if(slowTimer <= 0){
                slowTimer = SLOW_TIME;

                setIsSlowed(false);

            }
            else{
                slowTimer -= Time.deltaTime;
            }
        }
    }

        /*
    * Purpose: Has a timer that ticks down till the player is no longer slowed 
    */
    private void trippedUpdates(){
        if(isTripped){
            if(trippedTimer <= 0){
                trippedTimer = TRIPPED_TIME;
                isTripped = false;
            }
            else{
                trippedTimer -= Time.deltaTime;
            }
        }
    }

    /*
    * Creates the block of ice that incases the player and sets that flag to true
    */
    private void frozenUpdates(){
        if(isFrozen && !hasBeenFrozen){
            //Creates the object 
            Instantiate(frozenBrick, this.transform.position, Quaternion.identity);
            hasBeenFrozen = true;
        }
    }

    /*
    * Purpise: Update the direction the player is facing and which animation should be active 
    */
    private void spriteUpdates(){
                //Changes the running state 
        if(isHoldingBall && input == 0){
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsHolding", true);
        }
        else if(isHoldingBall && input != 0 ){
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsHolding", true);
        }
        else if(input != 0 && !isFrozen && !hasBeenFrozen){
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsHolding", false);
        }
        else{
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsHolding", false);
        }

        if(input > 0 && !isTripped){
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(input < 0 && !isTripped){
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    // Update is called once per frame
    //FixedUpdate function intreacts with the physics 
    /*
    *Purose: Update the rigidbody movment of the character 
    */
    void FixedUpdate()
    {
        //float input = Input.GetAxis("Horizontal"); Used for a transtional increase in speed 

        if(!isFrozen && !isTripped){
            input = Input.GetAxisRaw("Horizontal"); //Gets just -1 and 1 for movment or 0 if nothing is pressed 
            rigidbody.velocity = new Vector2(input * speed * speedSlow, rigidbody.velocity.y); //Updates the ridge body 
            zAngle = 0;
        }
        //If Frozen no movement 
        else if(isFrozen){
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
        //If is tripped slides in the last moved direction 
        else if(isTripped){
            if(trippedTimer >= TRIPPED_TIME/2f){
                rigidbody.velocity = new Vector2(input * speed * speedSlow, rigidbody.velocity.y); //Updates the ridge body 
            }
            else{
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
        
            if(input > 0){
                zAngle += 3; 
                if(zAngle > 90){
                    zAngle = 90;
                }
                transform.eulerAngles = new Vector3(0,0,zAngle);
             }
            else if(input < 0){
                zAngle -= 3; 
                if(zAngle < -90){
                    zAngle = -90;
                }
                transform.eulerAngles = new Vector3(0,180,zAngle);
            }
        }
    }

    

    public void damagePlayer(int damage){
        health -= damage;

        if(health <= 0){
            Destroy(gameObject);
        }
    }


    /*
    * Purpose: Changes the slow state that the player is in 
    * Input: Bool slowed : upadtes isSlowed  
    */
    public void setIsSlowed(bool slowed){
        isSlowed = slowed;
        if(isSlowed){
            head.color = new Color (0, 1, 0, 1); 
            body.color = new Color (0, 1, 0, 1); 
            rightArm.color = new Color (0, 1, 0, 1); 
            leftArm.color = new Color (0, 1, 0, 1); 
            rightLeg.color = new Color (0, 1, 0, 1); 
            leftLeg.color = new Color (0, 1, 0, 1); 

            speedSlow = 0.25f;
        }
        else{
            head.color = new Color (1, 1, 1, 1); 
            body.color = new Color (1, 1, 1, 1); 
            rightArm.color = new Color (1, 1, 1, 1); 
            leftArm.color = new Color (1, 1, 1, 1); 
            rightLeg.color = new Color (1, 1, 1, 1); 
            leftLeg.color = new Color (1, 1, 1, 1); 
            speedSlow = 1f;
        }
    }

    public float getInput(){
        return input;
    }

    public void setIsHoldingBall(bool visiblity){
        isHoldingBall = visiblity;
        if(isHoldingBall){
            ball.color = new Color (1, 1, 1, 1); 
        }
        else{
            ball.color = new Color (1, 1, 1, 0); 
        }
    }


}