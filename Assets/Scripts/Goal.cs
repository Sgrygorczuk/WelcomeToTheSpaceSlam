using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //============================= External Scripts 
    Player playerScript;

    public GameObject[] frontRow; 
    public GameObject[] midRow;  
    public GameObject[] backRow; 

    private bool startWave = false; 

    private int currentSpot = 0;

    public bool playerGoal;

    private float rise = 0;
    private float riseInc = 0.05f;
    private float[] intialY = new float[3]; 


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        intialY[0] = frontRow[0].transform.position.y;
        intialY[1] = midRow[0].transform.position.y;
        intialY[2] = backRow[0].transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
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
            startWave = true;
        }

    }
}
