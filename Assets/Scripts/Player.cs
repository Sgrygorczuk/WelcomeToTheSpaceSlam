using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    //============================== Vars 
    public float speed; 
    public int health;
    public bool isFrozen;
    private bool hasBeenFrozen;
    public bool isSlowed;
    private float speedSlow = 1;
    private float input;

    private float spawnTime;
    public float startSpawn;

    public GameObject frozenBrick;

    //============================= Referanced Object Traits 
    Rigidbody2D rigidbody;
    Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = startSpawn;
        animator = GetComponent<Animator>(); 
        rigidbody = GetComponent<Rigidbody2D>(); //Looks if the object has a RidgeBody2D component it can connect to 
    }

    // Update is called once per frame
    //Used for everything else 
    void Update()
    {
        //Changes the running state 
        if(input != 0 && !isFrozen && !hasBeenFrozen){
            animator.SetBool("IsRunning", true);
        }
        else{
            animator.SetBool("IsRunning", false);
        }

        if(input > 0){
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(input < 0){
            transform.eulerAngles = new Vector3(0,180,0);
        }

        if(isFrozen && !hasBeenFrozen){
                //Creates the object 
                Instantiate(frozenBrick, this.transform.position, Quaternion.identity);
                hasBeenFrozen = true;
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y); //Updates the ridge body 
        }

        if(isSlowed){
            if(spawnTime <= 0){
            spawnTime = startSpawn;

             setSlowed(false);
            }
            else{
                spawnTime -= Time.deltaTime;
            }
        }

    }

    // Update is called once per frame
    //FixedUpdate function intreacts with the physics 
    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal"); //Gets just -1 and 1 for movment or 0 if nothing is pressed 
        //float input = Input.GetAxis("Horizontal"); Used for a transtional increase in speed 

        if(!isFrozen){
        rigidbody.velocity = new Vector2(input * speed * speedSlow, rigidbody.velocity.y); //Updates the ridge body 
        }
    }

    public void damagePlayer(int damage){
        health -= damage;

        if(health <= 0){
            Destroy(gameObject);
        }
    }
    
    public void setFrozen(bool frozen){
        this.isFrozen = frozen;
    }

    public void setIsSlowed(bool slowed){
        this.isSlowed = slowed;
    }

    public void setHasBeenFrozen(bool frozen){
        this.hasBeenFrozen = frozen;
    }

    public bool getHasBeenFrozen(){return this.hasBeenFrozen;
    }

    public void setSlowed(bool slowed){
        isSlowed = slowed;
        if(isSlowed){
            //material.SetColor("_Color", Color.green);
            speedSlow = 0.1f;
        }
        else{
            speedSlow = 1f;
        }
    }

}
