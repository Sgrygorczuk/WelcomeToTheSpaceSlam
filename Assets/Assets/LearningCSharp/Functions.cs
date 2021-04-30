using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WeclomePlayer("Jack");
        WeclomePlayer("Fred");
        print(Addtion(1, 2));
    }

    void WeclomePlayer(string name)
    {
        print("Welcome " + name);
    }

    int Addtion(int number1, int number2)
    {
        return number1 + number2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
