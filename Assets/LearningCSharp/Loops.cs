using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loops : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int counter = 0;
        while(counter < 10)
        {
            print(counter);
            counter++;
        }

        for(int i = 0; i < 10; i++)
        {
            print(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
