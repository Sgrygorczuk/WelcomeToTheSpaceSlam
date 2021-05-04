using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    //============================ Object References 
    Player playerScript;
    Player playerScriptTwo;

    //================================ Crowds 
    public GameObject[] frontRow;  //Front row people 
    public GameObject[] midRow;    //Middle row people 
    public GameObject[] backRow;   //Back row 
    private bool startWave = false; //Should the wave play 
    private int currentSpot = 0;    //Which column are we in 
    private float rise = 0;         //How much should they rise 
    private float riseInc = 0.05f;  //Increment in which they rise adn fall 
    private float[] initialY = new float[3]; //The Y postion each row starts at 
    public bool playerGoal; //Did the player score 

    //================================ Game Count Down Timer 
    public Text timeText;  //The text in the middle of the screen 
    private float timer;   //Timer
    public float TIME;     //What the timer starts at 

    //==================================== End Screen 
    public GameObject loseScreen;   //End Game Screen 
    public Text endTeamText;        //Text that will be show for winning team
    public Text endScoreText;       //Winning Teams Score 
    Animator playerOne;             //Player One action 
    Animator playerTwo;             //Player Two Action 

    //===================================== Basket 
    public GameObject FX;       //Particle effect that players after score 
    private float initialXGoal; //Where the basket starts 
    private float initialYGoal; //Where the basket starts 
    AudioSource source;         //Sound FX that players when player scores 


    // Start is called before the first frame update
    /**
    *Purpose: Initialize 
    */
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerScriptTwo = GameObject.FindGameObjectWithTag("Player Two").GetComponent<Player>();

        playerOne = GameObject.FindGameObjectWithTag("P1").GetComponent<Animator>();
        playerTwo = GameObject.FindGameObjectWithTag("P2").GetComponent<Animator>();

        //Gets the initial y of each row 
        initialY[0] = frontRow[0].transform.position.y;
        initialY[1] = midRow[0].transform.position.y;
        initialY[2] = backRow[0].transform.position.y;

        //Gets the intial placement of the basketball basket 
        initialXGoal = transform.position.x;
        initialYGoal = transform.position.y;

        timer = TIME;
        timeText.text = timer.ToString();

        //End screen is off 
        loseScreen.SetActive(false);

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    /**
    *Purpose: Central update function 
    */
    void Update()
    {
        updateTimer();
        if(startWave){ updateWave(); }
        //Checks if the wave reached the end then resets it and turns it off 
        if(currentSpot == frontRow.Length && 
            frontRow[frontRow.Length -1].transform.position.y == frontRow[0].transform.position.y){
            currentSpot = 0;
            startWave = false;
        }
    }

    /**
    *Purpose: Updates the game timer and sets up the end screen upon it's completion 
    */
    void updateTimer(){
        if(timer <= 0){
            //Remove player control 
            playerScript.gameOver = true;
            playerScriptTwo.gameOver = true;
            //Show the screen 
            loseScreen.SetActive(true);

            //If player one wins 
            if(playerScript.playerScore > playerScriptTwo.playerScore){
                endTeamText.text = "Moon Mates Win";
                endScoreText.text = "Score: " + playerScript.playerScore.ToString();
                //If it's two player 
                if(!playerScriptTwo.isPlayerTwo)
                {
                    playerTwo.SetBool("Lost", true);
                }

            }
            //If they draw 
            else if(playerScript.playerScore == playerScriptTwo.playerScore){
                endTeamText.text = "Draw";
                endScoreText.text = "Score: " + playerScriptTwo.playerScore.ToString();
            }
            //If player two wins 
            else{
                endTeamText.text = "Star Strikers Win";
                endScoreText.text = "Score: " + playerScriptTwo.playerScore.ToString();
                playerOne.SetBool("Lost", true);
            }
        }
        //Count down and update the text with the time 
        else{
            timer -= Time.deltaTime;
            string seconds = Mathf.FloorToInt(timer%60) >= 10 ? (Mathf.FloorToInt(timer%60)).ToString() : 
                "0" + (Mathf.FloorToInt(timer%60)).ToString();
            
            timeText.text = timer > 0 ? (Mathf.FloorToInt(timer/60) + ":" + seconds) : "0:00";
        }
    }

    /**
    *Purpose: Update the crowd movement while in the wave 
    */
    void updateWave(){
        for(int i = 0; i < frontRow.Length; i++){
            //If we are in the current column make everyone rise a bit 
            if(i == currentSpot){
                frontRow[i].transform.position += new Vector3(0, riseInc, 0);
                midRow[i].transform.position += new Vector3(0, riseInc, 0);
                backRow[i].transform.position += new Vector3(0, riseInc, 0);
                rise += riseInc;
                //If we reach 0.5f height time to start the next column rise 
                if(rise > 0.5f){
                    rise = 0;
                    currentSpot++;
                }
            }
            //All other columns will either go down or remain at their initital ys 
            else{
                if(frontRow[i].transform.position.y > initialY[0] + riseInc){
                    frontRow[i].transform.position -= new Vector3(0, riseInc, 0);
                }
                else if(frontRow[i].transform.position.y < initialY[0] ){
                    frontRow[i].transform.position = new Vector3(frontRow[i].transform.position.x, initialY[0], 0);
                }

                if(midRow[i].transform.position.y > initialY[1] + riseInc){
                     midRow[i].transform.position -= new Vector3(0, riseInc, 0);
                }
                else if(midRow[i].transform.position.y < initialY[1]){
                     midRow[i].transform.position = new Vector3(midRow[i].transform.position.x, initialY[1], 0);
                }

                if(backRow[i].transform.position.y > initialY[2] + riseInc){
                     backRow[i].transform.position -= new Vector3(0, riseInc, 0);
                }
                else if(backRow[i].transform.position.y < initialY[2]){
                     backRow[i].transform.position = new Vector3(backRow[i].transform.position.x, initialY[2], 0);
                }
             }
        }
    }

    /**
    *Input: Hitbox 
    *Purpose: Updates the players collions 
    */
    void OnTriggerEnter2D(Collider2D hitbox){

        triggerUpdate(hitbox, "Player", playerScript);
        triggerUpdate(hitbox, "Player Two", playerScriptTwo);

    }

    /**
    *Input: Hitbox that will collide, 
            PlayerString, which player we're looking for
            Player, playerScript
    Purpose: Check the specific players collsion  
    */
    void triggerUpdate(Collider2D hitbox, string playerString, Player player){
        if(hitbox.tag == playerString && player.isHoldingBall && playerGoal){
            //Plays sound and makes the particle FX
            source.Play();
            Instantiate(FX, transform.position, Quaternion.identity);
                       
            //Removes ball from player, starts wave and increases player score 
            player.setIsHoldingBall(false);
            startWave = true;
            player.playerScore++;

            //Updates the score box 
            if(player.isPlayerTwo){
                player.playerscoreText.text =  player.playerscoreText.text = "Home: " + player.playerScore.ToString();
            }
            else{
                player.playerscoreText.text =  player.playerscoreText.text = "Away: " + player.playerScore.ToString();
            }

            //Moves the basket to the opposite side 
            if(transform.position.x == initialXGoal){
                this.transform.position = new Vector3(-initialXGoal, initialYGoal, 0);
                transform.eulerAngles = new Vector3(0,0,0); 
            }
            else{
                this.transform.position = new Vector3(initialXGoal, initialYGoal, 0);
                transform.eulerAngles = new Vector3(0,180,0);
            }
        }
    }
}
