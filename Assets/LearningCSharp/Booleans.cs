using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booleans : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bool statmentOne = 1 == 1;
        print(statmentOne);

        bool statmentTwo = 5 < 5;
        print(statmentTwo);

        bool statmentThree = 500 != 1;
        print(statmentThree);

        bool statmentFour = 500 != 1 && 2 == 2;
        print(statmentFour);

        bool statmentFive = 500 != 1 || 3 == 2;
        print(statmentFive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
