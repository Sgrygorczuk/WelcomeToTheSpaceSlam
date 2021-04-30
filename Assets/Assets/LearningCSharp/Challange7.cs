using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challange7 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    int SumArray(int[] array)
    {
        int sum = 0;
        for(int i = 0; i < array.Length; i++)
        {
            sum += array[i];
        }

        return sum;
    }

    void printReverse(string[] array)
    {
        for(int i = array.Length - 1; i > 0; i--)
        {
            print(array[i]);
        }
    }

    int Max(int[] array)
    {
        int max = array[0];
        
        for(int i = 1; i < array.Length; i++)
        {
            if(array[i] > max)
            {
                max = array[i];
            }
        }

        return max;
    }

    bool isUniform(string [] array)
    {
        string test = array[0];
        for(int i = 1; i < array.Length; i++)
        {
            if(test != array[i])
            {
                return false;
            }
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
