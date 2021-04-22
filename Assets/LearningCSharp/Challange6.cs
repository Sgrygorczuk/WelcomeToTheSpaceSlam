using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challange6 : MonoBehaviour
{

    bool isEven(int number)
    {
        return number % 2 == 0;
    }

    int whatIsBigger(int number1, int number2)
    {
        if(number1 > number2)
        {
            return number1;
        }
        else if(number1 < number2)
        {
            return number2;
        }
        else
        {
            return number1;
        }
    }

    void repeat(string name, int counter)
    {
        for(int i = 0; i < counter; i++)
        {
            print(name);
        }
    }

    int Factorial(int number)
    {
        int sum = 1; 
        for(int i = 1; i < number + 1; i++)
        {
            sum *= i;
        }

        return sum;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
