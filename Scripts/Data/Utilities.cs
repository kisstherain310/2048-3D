using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities 
{
    public static Vector3 toVector3(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }
    public static Quaternion toQuaternion(float x, float y, float z, float w)
    {
        return new Quaternion(x, y, z, w);
    }
    public static string ProcessNumber(int number)
    {
        string Number;
        if(10000 < number && number < 1000000){
            Number = (number / 1000).ToString() + "K";
        }else if(1000000 < number && number < 1000000000){
            Number = (number / 1000000).ToString() + "M";
        }else if(1000000000 < number){    
            Number = (number / 1000000000).ToString() + "B";
        }else{
            Number = number.ToString();
        }
        return Number;
    }
}
