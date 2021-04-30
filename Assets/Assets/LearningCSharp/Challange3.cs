using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challange3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        print((5 % 2 == 1 && 10 > 10) || (5 * 5 == 10 || "hey" == "hey")); //True 

        print(("Hello".Length >= 5 && 3.5f > 3.45f) && ("dragon".Length < 6 || "a" == "A")); //False 

        string name = "bob";
        string color = "blue";
        print((name.Length > color.Length || name[0] == color[0]) || (name[2] == color[0] && 1 != -1)); //True 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
