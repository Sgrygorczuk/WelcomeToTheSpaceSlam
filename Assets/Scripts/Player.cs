using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    //============================== Vars 
    private float xInput = 0;
    private float yInput = 0;
    public bool isInAir = false;
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

    //=========================== Immune 
    private float immuneTimer;
    public float IMMUNE_TIME;
    public bool isImmune;

    public int currentColorChange = 0; 
    private float r = 1;
    private float g = 0; 
    private float b = 0;

    //======================= Speedy

    public bool isSpeedy;

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

    // Start is called before the first frame update

    public TrailRenderer trailRenderer;

    void Start()
    {
        immuneTimer = IMMUNE_TIME;
        slowTimer = SLOW_TIME;
        trippedTimer = TRIPPED_TIME;
        animator = GetComponent<Animator>();     //Looks for the animatior object connected to the player 
        rigidbody = GetComponent<Rigidbody2D>(); //Looks if the object has a RidgeBody2D component it can connect to 
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.time = 0;
        ball.color = new Color (0, 0, 0, 0); 
    }

    // Update is called once per frame
    //Used for everything else 
    void Update()
    {
        spriteUpdates(); 
        slowedSpeedyUpdates();
        frozenUpdates();
        trippedUpdates();
        immuneUpdates();
    }

    /*
    * Purpose: Has a timer that ticks down till the player is no longer slowed 
    */
    private void slowedSpeedyUpdates(){
        if(isSlowed || isSpeedy){
            if(slowTimer <= 0){
                slowTimer = SLOW_TIME;

                if(isSlowed){
                     setIsSlowed(false);
                }
                if(isSpeedy){
                    setIsSpeedy(false);
                }

            }
            else{
                slowTimer -= Time.deltaTime;
            }
        }
    }


    /*
    * Purpose: 
    */
    private void immuneUpdates(){
       if(isImmune){
        /*
        *0: R is 255, G is 0, but increaseing, B is 0 
        *1: R is 255 Decreaseing G is 255, B is 0
        *2: R is 0 Decreaseing G is 255, B is 0 increaseing
        *3: R is 0, G is 255 Decreaseing, B is 255
        *4: R is 0 increaseing G is 0, B is 255
        *5: R is 255, G is 0, B is 255 Decreaseing
        */
            switch(currentColorChange){
                case 0:{
                    g += 0.05f;
                    if(g >= 1){
                        g = 1;
                        currentColorChange++;
                    }
                    break;
                }
                case 1:{
                    r -= 0.05f;
                    if(r <= 0){
                        r = 0;
                        currentColorChange++;
                    }
                    break;
                }
                case 2:{
                    b += 0.05f;
                    if(b >= 1){
                        b = 1;
                        currentColorChange++;
                    }
                    break;
                }
                case 3:{
                    g -= 0.05f;
                    if(g <= 0 ){
                        g = 0;
                        currentColorChange++;
                    }
                    break;
                }
                case 4:{
                    r += 0.05f;
                    if(r >= 1){
                        r = 1;
                        currentColorChange++;
                    }
                    break;
                }
                case 5:{
                    b -= 0.05f;
                    if(b <= 0){
                        b = 0;
                        currentColorChange = 0;
                    }
                    break;
                }
            }

            head.color = new Color (r, g, b, 1); 
            body.color = new Color (r, g, b, 1);
            rightArm.color = new Color (r, g, b, 1);
            leftArm.color = new Color (r, g, b, 1);
            rightLeg.color = new Color (r, g, b, 1);
            leftLeg.color = new Color (r, g, b, 1);

            trailRenderer.startColor = new Color (r, g, b, 0.1f);

            if(immuneTimer <= 0){
                immuneTimer = IMMUNE_TIME;
                isImmune = false;
                trailRenderer.time = 0;
                head.color = new Color (1, 1, 1, 1); 
                body.color = new Color (1, 1, 1, 1); 
                rightArm.color = new Color (1, 1, 1, 1); 
                leftArm.color = new Color (1, 1, 1, 1); 
                rightLeg.color = new Color (1, 1, 1, 1); 
                leftLeg.color = new Color (1, 1, 1, 1); 

            }
            else{
                immuneTimer -= Time.deltaTime;
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
        if(isHoldingBall && xInput == 0){
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsHolding", true);
        }
        else if(isHoldingBall && xInput != 0 ){
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsHolding", true);
        }
        else if(xInput != 0 && !isFrozen && !hasBeenFrozen){
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsHolding", false);
        }
        else{
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsHolding", false);
        }

        if(xInput > 0 && !isTripped){
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(xInput < 0 && !isTripped){
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
            xInput = Input.GetAxisRaw("Horizontal"); //Gets just -1 and 1 for movment or 0 if nothing is pressed 
            zAngle = 0;
            if (Input.GetKey(KeyCode.UpArrow) && !isInAir)
            {
               yInput = 15; 
               isInAir = true;
            }
            else{
                yInput = rigidbody.velocity.y;
            }

            rigidbody.velocity = new Vector2(xInput * speed * speedSlow, yInput); //Updates the ridge body 
        }
        //If Frozen no movement 
        else if(isFrozen){
            rigidbody.velocity = new Vector2(0, 0.5f);
        }
        //If is tripped slides in the last moved direction 
        else if(isTripped){
            if(trippedTimer >= TRIPPED_TIME/2f){
                rigidbody.velocity = new Vector2(xInput * speed * speedSlow, rigidbody.velocity.y); //Updates the ridge body 
            }
            else{
                rigidbody.velocity = new Vector2(0, 0);
            }
        
            if(xInput > 0){
                zAngle += 3; 
                if(zAngle > 90){
                    zAngle = 90;
                }
                transform.eulerAngles = new Vector3(0,0,zAngle);
             }
            else if(xInput < 0){
                zAngle -= 3; 
                if(zAngle < -90){
                    zAngle = -90;
                }
                transform.eulerAngles = new Vector3(0,180,zAngle);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitbox){
        if(hitbox.tag == "Ground"){
            isInAir = false;
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

        /*
    * Purpose: Changes the slow state that the player is in 
    * Input: Bool slowed : upadtes isSlowed  
    */
    public void setIsSpeedy(bool speedy){
        isSpeedy = speedy;
        if(isSpeedy){
            head.color = new Color (1, 0, 0, 1); 
            body.color = new Color (1, 0, 0, 1); 
            rightArm.color = new Color (1, 0, 0, 1); 
            leftArm.color = new Color (1, 0, 0, 1); 
            rightLeg.color = new Color (1, 0, 0, 1); 
            leftLeg.color = new Color (1, 0, 0, 1); 

            speedSlow = 1.5f;
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
        return xInput;
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