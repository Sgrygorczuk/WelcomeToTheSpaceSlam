using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    //============================== Vars 
    public float speed; 
    public int health;
    private float input;

    //============================= Referanced Object Traits 
    Rigidbody2D rigidbody;
    Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        rigidbody = GetComponent<Rigidbody2D>(); //Looks if the object has a RidgeBody2D component it can connect to 
    }

    // Update is called once per frame
    //Used for everything else 
    void Update()
    {
        //Changes the running state 
        if(input != 0){
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
    }

    // Update is called once per frame
    //FixedUpdate function intreacts with the physics 
    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal"); //Gets just -1 and 1 for movment or 0 if nothing is pressed 
        //float input = Input.GetAxis("Horizontal"); Used for a transtional increase in speed 

        print(input);

        rigidbody.velocity = new Vector2(input * speed, rigidbody.velocity.y); //Updates the ridge body 
    }

    public void damagePlayer(int damage){
        health -= damage;

        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
