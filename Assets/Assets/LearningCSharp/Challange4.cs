using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challange4 : MonoBehaviour
{

    public int number1;
    public int numebr2;

    [Range(0, 100)] //Sets the range of the input to be only between 0 and 100 
    public int grade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void isGreaterThan()
    {
        if(number1 == numebr2)
        {
            print("Number 1 is equal to Number 2");
        }
        else if(number1 > numebr2)
        {
            print("Numebr 1 is greater than Number 2");
        }
        else
        {
            print("Numebr 1 i less than Number 2");
        }
    }

    void returnGrade()
    {
        if (grade > 100)
        {
            grade = 100;
        }

        if (grade < 0)
        {
            grade = 0;
        }

        if (grade >= 0 && grade < 65)
        {
            print("Grade: E");
        }
        else if (grade >= 65 && grade < 70)
        {
            print("Grade: D");
        }
        else if (grade >= 70 && grade < 80)
        {
            print("Grade: C");
        }
        else if (grade >= 80 && grade < 90)
        {
            print("Grade: B");
        }
        else if (grade >= 90 && grade <= 100)
        {
            print("Grade: A");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
