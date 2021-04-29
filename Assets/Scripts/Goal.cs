using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    //============================= External Scripts 
    Player playerScript;

    Player playerScriptTwo;

    public GameObject[] frontRow; 
    public GameObject[] midRow;  
    public GameObject[] backRow; 

    public GameObject FX; 

    public Text timeText; 
    private float timer;
    public float TIME;

    private bool startWave = false; 

    private int currentSpot = 0;

    public bool playerGoal;

    private float rise = 0;
    private float riseInc = 0.05f;
    private float[] intialY = new float[3]; 

    private float initalXGoal;
    private float intialYGoal;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerScriptTwo = GameObject.FindGameObjectWithTag("Player Two").GetComponent<Player>();

        intialY[0] = frontRow[0].transform.position.y;
        intialY[1] = midRow[0].transform.position.y;
        intialY[2] = backRow[0].transform.position.y;

        initalXGoal = transform.position.x;
        intialYGoal = transform.position.y;

        timer = TIME;
        timeText.text = timer.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if(timer <= 0){
        }
        else{
            timer -= Time.deltaTime;
            timeText.text = (Mathf.Round((timer/60) * 10.0f) * 0.1f).ToString();
        }

        if(startWave){
            for(int i = 0; i < frontRow.Length; i++){
                if(i == currentSpot){
                    frontRow[i].transform.position += new Vector3(0, riseInc, 0);
                    midRow[i].transform.position += new Vector3(0, riseInc, 0);
                    backRow[i].transform.position += new Vector3(0, riseInc, 0);
                    rise += riseInc;
                    if(rise > 0.5f){
                        rise = 0;
                        currentSpot++;
                    }
                }
                else{
                    if(frontRow[i].transform.position.y > intialY[0] + riseInc){
                            frontRow[i].transform.position -= new Vector3(0, riseInc, 0);
                    }
                    else if(frontRow[i].transform.position.y < intialY[0] ){
                        frontRow[i].transform.position = new Vector3(frontRow[i].transform.position.x, intialY[0], 0);
                    }

                    if(midRow[i].transform.position.y > intialY[1] + riseInc){
                         midRow[i].transform.position -= new Vector3(0, riseInc, 0);
                    }
                    else if(midRow[i].transform.position.y < intialY[1]){
                        midRow[i].transform.position = new Vector3(midRow[i].transform.position.x, intialY[1], 0);
                    }


                    if(backRow[i].transform.position.y > intialY[2] + riseInc){
                        backRow[i].transform.position -= new Vector3(0, riseInc, 0);
                    }
                    else if(backRow[i].transform.position.y < intialY[2]){
                        backRow[i].transform.position = new Vector3(backRow[i].transform.position.x, intialY[2], 0);
                    }
                
                }

            }
        }
        if(currentSpot == frontRow.Length && frontRow[frontRow.Length -1].transform.position.y == frontRow[0].transform.position.y){
            currentSpot = 0;
            startWave = false;
        }
    }

    void OnTriggerEnter2D(Collider2D hitbox){
        
        if(hitbox.tag == "Player" && playerScript.isHoldingBall && playerGoal){
            playerScript.setIsHoldingBall(false);
            Instantiate(FX, transform.position, Quaternion.identity);
            startWave = true;
            playerScript.playerScore++;
            if(playerScript.isPlayerTwo){
                playerScript.playerscoreText.text =  playerScript.playerscoreText.text = "Away: " + playerScript.playerScore.ToString();

            }
            else{
                playerScript.playerscoreText.text =  playerScript.playerscoreText.text = "Home: " + playerScript.playerScore.ToString();
            }
            if(transform.position.x == initalXGoal){
                this.transform.position = new Vector3(-initalXGoal, intialYGoal, 0);
                transform.eulerAngles = new Vector3(0,0,0); 
            }
            else{
                this.transform.position = new Vector3(initalXGoal, intialYGoal, 0);
                transform.eulerAngles = new Vector3(0,180,0);
            }
        }
 
        if(hitbox.tag == "Player Two" && playerScriptTwo.isHoldingBall && playerGoal){
            playerScriptTwo.setIsHoldingBall(false);
            Instantiate(FX, transform.position, Quaternion.identity);
            startWave = true;
            playerScriptTwo.playerScore++;
            if(playerScriptTwo.isPlayerTwo){
                playerScriptTwo.playerscoreText.text =  playerScriptTwo.playerscoreText.text = "Away: " + playerScriptTwo.playerScore.ToString();

            }
            else{
                playerScriptTwo.playerscoreText.text =  playerScriptTwo.playerscoreText.text = "Home: " + playerScriptTwo.playerScore.ToString();
            }
            if(transform.position.x == initalXGoal){
                this.transform.position = new Vector3(-initalXGoal, intialYGoal, 0);
                transform.eulerAngles = new Vector3(0,0,0); 
            }
            else{
                this.transform.position = new Vector3(initalXGoal, intialYGoal, 0);
                transform.eulerAngles = new Vector3(0,180,0);
            }
        }

    }
}
