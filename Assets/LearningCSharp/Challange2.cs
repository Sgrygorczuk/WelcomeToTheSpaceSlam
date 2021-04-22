using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challange2 : MonoBehaviour
{

    public string firstName = "Joe";
    public string lastName = "Shmoe";
    public int yearOfBirth = 1995;

    // Start is called before the first frame update
    void Start()
    {
        print("Your name is " + firstName + " " + lastName);
        print("Your initals are " + firstName[0] + lastName[0]);
        print("Your full name is the lenght of " + (firstName.Length + lastName.Length) + " characters");
        print("A random character in your first name is " + firstName[UnityEngine.Random.Range(0, firstName.Length - 1)]);
        print("Your were born in " + yearOfBirth + " that means you're " + (DateTime.Now.Year - yearOfBirth));
        print("You have been alive at least " + (DateTime.Now.Year - yearOfBirth) * 365 + " days");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
