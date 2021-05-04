using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{    
    //============================== Vars 
    private float xInput = 0; //X input speed 
    private float yInput = 0; //Y input speed 
    public bool isInAir = false; //Tells us if we're in the air 
    public float speed;         //general speed 

    //========================== Frozen 
    public bool isFrozen;       //Tells us if the player has been frozen 
    public bool hasBeenFrozen;  //Used to stop spawning the brick after first one 

    //============================ SpeedM Mod
    private float speedTimer;   //Timer 
    public float SPEED_MOD_TIME;    //How long it will last 
    private float speedModifier = 1;    //Speed change effect 
    public bool isSpeedy; //Tells us that the player has the fast buff on them
    public bool isSlowed;  //Tells us if the player is slowed  

    //============================== Trip 
    private float trippedTimer;     //Timer 
    public float TRIPPED_TIME;      //How long the timer lasts
    public bool isTripped;          //Tells us if the player is tripped 
    private float zAngle;           //Rotation of the player 

    //========================= Holding Ball 
    public bool isHoldingBall;     //Tells us if the ball is visible and can be dunked 

    //=========================== Immune 
    private float immuneTimer;  //Timer 
    public float IMMUNE_TIME;   //How long the timer will last 
    public bool isImmune;       //Is the immune state on 
 
    public int currentColorChange = 0; //Which transition of color change we 
    private float r = 1;
    private float g = 0; 
    private float b = 0;

    //============================= Reference Object Traits 
    public GameObject frozenBrick;  //Spawns a block of ice 
    Rigidbody2D rigidbody;  //Used for physics 
    Animator animator; //Used to update the given animation  

    //============================== Sprite
    public SpriteRenderer head;
    public SpriteRenderer body;
    public SpriteRenderer leftArm;
    public SpriteRenderer rightArm;
    public SpriteRenderer leftLeg;
    public SpriteRenderer rightLeg;
    public SpriteRenderer ball;

    public TrailRenderer trailRenderer; //Reference to the tail object 
    

    //=================== Score 
    public Text playerscoreText;  //Reference to the text object 

    public int playerScore = 0;   //Holds player score 

    //=================== Control 

    public bool isPlayerTwo;      //Tells us which control scheme to use 

    public bool gameOver = false; //Checks if the game timer ran out 

    /**
    *Initialize the timers, grab the player scrips
    */
    void Start()
    {   
        //Timers 
        immuneTimer = IMMUNE_TIME;
        speedTimer = SPEED_MOD_TIME;
        trippedTimer = TRIPPED_TIME;


        animator = GetComponent<Animator>();     //Looks for the animator object connected to the player 
        rigidbody = GetComponent<Rigidbody2D>(); //Looks if the object has a RidgeBody2D component it can connect to 
        trailRenderer = GetComponent<TrailRenderer>();  //Looks for the tail 
       
       
        trailRenderer.time = 0;              //Set rander time 
        ball.color = new Color (0, 0, 0, 0); //Sets color of the ball

        //Score board initial text 
        if(isPlayerTwo){
            playerscoreText.text = "Home: " + playerScore.ToString();
        }
        else{
            playerscoreText.text = "Away: " + playerScore.ToString();
        }
    }

    // Update is called once per frame
    //Used for everything else 
    void Update()
    {
        if(!gameOver){
            spriteUpdates(); 
            slowedSpeedyUpdates();
            frozenUpdates();
            trippedUpdates();
            immuneUpdates();
        }
    }

    /**
    * Purpose: Has a timer that ticks down till the player is no longer slowed 
    */
    private void slowedSpeedyUpdates(){
        if(isSlowed || isSpeedy){
            if(speedTimer <= 0){
                speedTimer = SPEED_MOD_TIME;

                if(isSlowed){
                    setIsSlowed(false);
                }
                if(isSpeedy){
                    setIsSpeedy(false);
                }

            }
            else{
                speedTimer -= Time.deltaTime;
            }
        }
    }


    /**
    * Purpose: If player hits rainbow ball they fly through a rainbow of colors 
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
            
            setBodyColor(r,g,b,1);
            trailRenderer.startColor = new Color (r, g, b, 0.1f);

            if(immuneTimer <= 0){
                immuneTimer = IMMUNE_TIME;
                isImmune = false;
                trailRenderer.time = 0f;
                setBodyColor(1,1,1,1);
            }
            else{
                immuneTimer -= Time.deltaTime;
            }
        }
    }

    /**
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

    /**
    * Creates the block of ice that incases the player and sets that flag to true
    */
    private void frozenUpdates(){
        if(isFrozen && !hasBeenFrozen){
            //Creates the object 
            Instantiate(frozenBrick, this.transform.position, Quaternion.identity);
            hasBeenFrozen = true;
        }
    }

    /**
    * Purpose: Update the direction the player is facing and which animation should be active 
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

        //Changes which way the player is facing 
        if(xInput > 0 && !isTripped){
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(xInput < 0 && !isTripped){
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    // Update is called once per frame
    //FixedUpdate function intreacts with the physics 
    /**
    *Purpose: Update the rigidbody movement of the character 
    */
    void FixedUpdate()
    {
        //Makes sure the game is not done 
        if(!gameOver){
            if(!isFrozen && !isTripped){
                regularUpdate();
            }
            //If Frozen no movement 
            else if(isFrozen){
                rigidbody.velocity = new Vector2(0, 0.3f);
            }
            //If is tripped slides in the last moved direction 
            else if(isTripped){
                trippedUpdate();
            }
        }
    }

    /**
    *Purpose: Updates the player movement unimpaired 
    */
    void regularUpdate(){
        //Read the player input and move them 
        if((Input.GetKey(KeyCode.A) && isPlayerTwo) || (Input.GetKey(KeyCode.LeftArrow) && !isPlayerTwo)){
            xInput = -1;
        }
        else if((Input.GetKey(KeyCode.D) && isPlayerTwo) ||(Input.GetKey(KeyCode.RightArrow) && !isPlayerTwo) ){
            xInput = 1;
        }
        else{
            xInput = 0;
        }

        //Make sure the player is upright 
        zAngle = 0;

        //Jumping 
        if ((!isPlayerTwo && Input.GetKey(KeyCode.UpArrow) && !isInAir) || (isPlayerTwo && Input.GetKey(KeyCode.W) && !isInAir))
        {
            yInput = 8; 
            isInAir = true;
        }
        else{
            yInput = rigidbody.velocity.y;
        }

        //Pass all the info into the rigidbody 
        rigidbody.velocity = new Vector2(xInput * speed * speedModifier, yInput); //Updates the ridge body 
    }

    /**
    *Purpose: Updates the player movement if they were tripped 
    */
    void trippedUpdate(){
        //For the first half keep the velocity the player initially had 
         if(trippedTimer >= TRIPPED_TIME/2f){
            rigidbody.velocity = new Vector2(xInput * speed * speedModifier, rigidbody.velocity.y); //Updates the ridge body 
        }
        //After that velocity is 0
        else{
            rigidbody.velocity = new Vector2(0, 0);
        }
        
        //If the input is to the right rotate to the right 
        if(xInput > 0){
            zAngle += 3; 
            if(zAngle > 90){
                 zAngle = 90;
            }
            transform.eulerAngles = new Vector3(0,0,zAngle);
        }
        //Else rotate to the left 
        else if(xInput < 0){
            zAngle -= 3; 
            if(zAngle < -90){
                zAngle = -90;
            }
            transform.eulerAngles = new Vector3(0,180,zAngle);
        }
    }

    /**
    *Input: hitbox
    *Purpose: Checks for collision with the floor to reset the jump flag 
    */
    void OnTriggerEnter2D(Collider2D hitbox){
        if(hitbox.tag == "Ground"){
            isInAir = false;
        }
    }

    /**
    * Purpose: Changes the slow state that the player is in 
    * Input: Bool slowed : updates isSlowed  
    */
    public void setIsSlowed(bool slowed){
        isSlowed = slowed;
        //Set player to be green and slowed 
        if(isSlowed){
            setBodyColor(0,1,0,1);
            speedModifier = 0.25f;
        }
        //Sets player to be normal color and normal speed 
        else{
            setBodyColor(1,1,1,1);
            speedModifier = 1f;
        }
    }

    /**
    * Purpose: Changes the slow state that the player is in 
    * Input: Bool slowed : updates isSlowed  
    */
    public void setIsSpeedy(bool speedy){
        isSpeedy = speedy;
        //If speed is on turn player red and make them faster 
        if(isSpeedy){
            setBodyColor(1, 0, 0, 1);
            speedModifier = 1.5f;
        }
        //Restart player to normal color and make their speed regular
        else{
            setBodyColor(1,1,1,1);
            speedModifier = 1f;
        }
    }

    /**
    *Returns uses xInput (x Speed)
    */
    public float getInput(){ return xInput; }

    /**
    *Input: Visibility, tell us if the ball can be seen or not 
    *Purpose: Make the ball that the player is carrying visible or not 
    */
    public void setIsHoldingBall(bool visibility){
        isHoldingBall = visibility;
        if(isHoldingBall){
            ball.color = new Color (1, 1, 1, 1); 
        }
        else{
            ball.color = new Color (1, 1, 1, 0); 
        }
    }

    /**
    *Input: r, g, b, values for red green blue and alpha 
    *Purpose: Sets the color of the body 
    */
    void setBodyColor(float r, float g, float b, float a){
            head.color = new Color (r, g, b, a); 
            body.color = new Color (r, g, b, a); 
            rightArm.color = new Color (r, g, b, a); 
            leftArm.color = new Color (r, g, b, a); 
            rightLeg.color = new Color (r, g, b, a); 
            leftLeg.color = new Color (r, g, b, a); 
    }


}