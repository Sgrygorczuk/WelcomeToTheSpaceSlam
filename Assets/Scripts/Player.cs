using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed; 
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); //Looks if the object has a RidgeBody2D component it can connect to 
    }

    // Update is called once per frame
    //FixedUpdate function intreacts with the physics 
    void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal"); //Gets just -1 and 1 for movment or 0 if nothing is pressed 
        //float input = Input.GetAxis("Horizontal"); Used for a transtional increase in speed 

        print(input);

        rigidbody.velocity = new Vector2(input * speed, rigidbody.velocity.y); //Updates the ridge body 
    }
}
