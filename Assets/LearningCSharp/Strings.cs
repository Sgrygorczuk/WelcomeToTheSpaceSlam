using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("This is a string");
        print("String".Length + "Another String".Length);
        print("My Body is my Soul".Substring(3, 7));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
