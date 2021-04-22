using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challange5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = -5; i < 6; i++)
        {
            print(i);
        }

        for(int i = 10; i < 51; i += 2) {
            print(i);
        }

        for(int i = 0; i < 100; i++)
        {
            if(i % 3 == 0 && i % 5 == 0)
            {
                print(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
