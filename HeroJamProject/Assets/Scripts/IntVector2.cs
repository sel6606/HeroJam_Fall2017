using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Struct that allows us to make a vector2 of ints
/// </summary>
[System.Serializable]
public struct IntVector2
{

    public int x;
    public int y;

    //Constructor
    public IntVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Overload the '+' operator for the IntVector2 struct
    /// </summary>
    /// <param name="a">The vector on the left side of the equation</param>
    /// <param name="b">The vector on the right side of the equation</param>
    /// <returns>The result of the vector addition</returns>
    public static IntVector2 operator + (IntVector2 a, IntVector2 b)
    {
        IntVector2 temp = new IntVector2();

        temp.x = a.x + b.x;
        temp.y = a.y + b.y;
        return temp;
    }

    /// <summary>
    /// Overload the '==' operator for the IntVector2 struct
    /// </summary>
    /// <param name="a">The vector on the left side of the equation</param>
    /// <param name="b">The vector on the right side of the equation</param>
    /// <returns>True, if they have the same x and y value</returns>
    public static bool operator ==(IntVector2 a, IntVector2 b)
    {
        if (a.x == b.x && a.y == b.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Overload the '!=' operator for the IntVector2 struct
    /// </summary>
    /// <param name="a">The vector on the left side of the equation</param>
    /// <param name="b">The vector on the right side of the equation</param>
    /// <returns>True, if they have different x or y values</returns>
    public static bool operator !=(IntVector2 a, IntVector2 b)
    {
        if (a == b)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Override the Equals operator
    /// </summary>
    /// <param name="obj">The object</param>
    /// <returns>True or false</returns>
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }


    /// <summary>
    /// Override the GetHashCode method
    /// </summary>
    /// <returns>The hash code</returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}
