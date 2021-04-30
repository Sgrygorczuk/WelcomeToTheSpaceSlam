using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{

    public string name = "Seba";
    public string wepon = "Stick";
    public int health = 100;
    public float speed = 5.5f;
    public int enemyDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        print("Welcome " + name);
        print("You were caught by a goblin beacue your speed is too slow: " + speed.ToString());

        health -= enemyDamage;

        print("Enemy dealt damage " + enemyDamage.ToString() + " leaving you off with meager " + health.ToString());
        print("After a struggle you were able to slay the monster using your" + wepon);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
