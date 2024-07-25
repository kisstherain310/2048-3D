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
}
