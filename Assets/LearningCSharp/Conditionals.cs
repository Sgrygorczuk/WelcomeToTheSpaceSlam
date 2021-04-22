using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditionals : MonoBehaviour
{

    public int number1;
    public int number2;

    public string action;

    // Start is called before the first frame update
    void Start()
    {
        switch (action)
        {
            case "add":
                {
                    print(number1 + number2);
                    break;
                }
            case "subtract":
                {
                    print(number1 - number2);
                    break;
                }
            case "multiply":
                {
                    print(number1 * number2);
                    break;
                }
            case "divide":
                {
                    if (number2 != 0)
                    {
                        print(number1 + number2);
                    }
                    else
                    {
                        print("You can't multiply by 0");
                    }
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
