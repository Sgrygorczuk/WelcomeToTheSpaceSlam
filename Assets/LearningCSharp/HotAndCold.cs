using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotAndCold : MonoBehaviour
{

    int randomNumber;

    [Range(0,100)]
    public int guess;

    int lastGuess = 0;

    // Start is called before the first frame update
    void Start()
    {

        randomNumber = Random.Range(0, 101);
        print("Welcome to the hot and cold guessing game");
        print("I will think of a number between 0 and 100 and you have to guess it. " + randomNumber);
        print("Enter your guess and click [SPACE] I'll tell you how close you are! Good luck!");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(guess == randomNumber)
            {
                print("You got it!");
            }
            else if(Mathf.Abs(randomNumber - lastGuess) > Mathf.Abs(randomNumber - guess))
            {
                print("Colder");
            }
            else
            {
                print("Warmer");
            }

            lastGuess = guess;

        }
    }
}
