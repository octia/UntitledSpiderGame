using Unity.VisualScripting;
using UnityEngine;

public static class Vector3Extensions
{
    
    public static Vector3 SwapYZ(this Vector3 vector)
    {
        var toReturn = new Vector3(vector.x, vector.z, vector.y);

        return toReturn;
    }

    public static Vector3 WithX(this Vector3 vector, int newX)
    {
        var toReturn = vector;
        toReturn.x = newX;
        return toReturn;
    }

        public static Vector3 WithY(this Vector3 vector, int newY)
    {
        var toReturn = vector;
        toReturn.y = newY;
        return toReturn;
    }

        public static Vector3 WithZ(this Vector3 vector, int newZ)
    {
        var toReturn = vector;
        toReturn.z = newZ;
        return toReturn;
    }


}
